using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Entities;
using WebApi.Entities.Configurations;

namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<User> Users { get; set; }

        public DbSet<RegistrationReferrer> RegistrationReferrers { get; set; }


        public void OnAuthModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RegistrationReferrerConfiguration());
        }
    }
}