using Steeltoe.Discovery.Client;

namespace ForumService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                //.AddServiceDiscovery(options => options.UseEureka())
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
                //.AddDiscoveryClient();
    }
}