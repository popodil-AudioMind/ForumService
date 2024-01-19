using ForumService.Context;
using Microsoft.EntityFrameworkCore;

namespace ForumService.Services
{
    public static class DatabaseManagementService
    {
        public static void MigrationInitialisation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var database = serviceScope.ServiceProvider.GetService<ForumDatabaseContext>().Database;
                var migrations = database.GetMigrations();
                if (migrations == null)
                {
                    database.Migrate();
                }
            }
        }
    }
}
