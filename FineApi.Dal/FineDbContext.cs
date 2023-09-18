using System.Reflection;
using FineApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FineApi.Dal;
public class FineDbContext : DbContext
{
    public FineDbContext(DbContextOptions<FineDbContext> options):base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public DbSet<UserCarInformation> UserCarInformation { get; set; }
    public DbSet<ReceivedSms> ReceivedSms { get; set; }
    public DbSet<Company> Company { get; set; }
}
