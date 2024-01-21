using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using ForumService.Context;
using ForumService.Services;
using System.Net;
using ForumService.Data;
using ForumService.SQL;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using MassTransit;
using Audiomind.RabbitMQ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Prometheus;
using Microsoft.EntityFrameworkCore.Migrations;
using Cassandra;

namespace ForumService
{
    public class Startup
    {
        private IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.SetIsOriginAllowed((host) => true)
                        //.WithOrigins("http://localhost:19006/")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            //DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            /*string connectionstring;
            //if (_env.IsDevelopment()) connectionstring = Configuration.GetValue<string>("ConnectionStrings:DevConnection");
            //else connectionstring = Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            connectionstring = Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            try
            {
                services.AddDbContextPool<ForumDatabaseContext>(
                    options => options.UseMySql(connectionstring.ToString(), ServerVersion.AutoDetect(connectionstring))
                    *//*options => options.UseCassandra("Contact Points=127.0.0.1;", "cv", opt =>
                    {
                        opt.MigrationsHistoryTable(HistoryRepository.DefaultTableName);
                    }, o =>
                    {

                        o.WithQueryOptions(new QueryOptions().SetConsistencyLevel(ConsistencyLevel.LocalOne))
                            .WithReconnectionPolicy(new ConstantReconnectionPolicy(1000))
                            .WithRetryPolicy(new DefaultRetryPolicy())
                            .WithLoadBalancingPolicy(new TokenAwarePolicy(Policies.DefaultPolicies.LoadBalancingPolicy))
                            .WithDefaultKeyspace(GetType().Name)
                            .WithPoolingOptions(
                            PoolingOptions.Create()
                                .SetMaxSimultaneousRequestsPerConnectionTreshold(HostDistance.Remote, 1_000_000)
                                .SetMaxSimultaneousRequestsPerConnectionTreshold(HostDistance.Local, 1_000_000)
                                .SetMaxConnectionsPerHost(HostDistance.Local, 1_000_000)
                                .SetMaxConnectionsPerHost(HostDistance.Remote, 1_000_000)
                                .SetMaxRequestsPerConnection(1_000_000)
                        );
                    }
                    )*//*
                );
            }
            catch (Exception)
            {
                throw;
            }*/

            services.AddScoped<ISqlForum, SqlForum>();
            services.AddScoped<IMessageProducer, RabbitMQProducer>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = null,
            ValidAudience = null,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JWTSecurityKey")))
        };
    });
            services.AddAuthorization();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestAPI", Version = "v1" });
            });
            services.AddSignalR();


            services.AddMassTransit(x =>
            {
                x.AddConsumer<DeleteConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            //services.AddDiscoveryClient(Configuration);
            //services.AddDiscoveryClient();
            //services.AddServiceDiscovery(options => options.UseEureka());

            /*services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
                options.HttpsPort = 443;
            });*/
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //DatabaseManagementService.MigrationInitialisation(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestAPI v1"));


            app.UseRouting();
            app.UseHttpMetrics();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMetrics();
            });
        }
    }
}