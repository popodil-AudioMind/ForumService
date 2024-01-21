using Microsoft.EntityFrameworkCore;
using ForumService.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumService.Context
{
    public class ForumDatabaseContext : DbContext
    {
        public ForumDatabaseContext(DbContextOptions<ForumDatabaseContext> options) : base(options)
        { 
        
        }
        public DbSet<IForum> Forums { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<IUser>()
                .Property(e => e.id)
                .HasConversion(
                    v => v.ToString(),
                    v => Guid.Parse(v));
        }*/
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCassandra("Contact Points=127.0.0.1;", "cv", opt =>
            {
                opt.MigrationsHistoryTable(HistoryRepository.DefaultTableName);
            }, o => {

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
            });
        }*/
    }

}
