using System.Reflection;
using Fine.Api.DataAccess.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fine.Api.DataAcess.DbContexts
{
    public class FineDbContext : DbContext
    {
        public FineDbContext(DbContextOptions<FineDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<UserCarInformation> UserCarInformations { get; set; }
        public DbSet<ReceivedSms> ReceivedSms { get; set; }
    }
}
