using System.Reflection;
using FineApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FineApi.Dal;
public class FineDbContext : DbContext
{
    public FineDbContext(DbContextOptions<FineDbContext> options):base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    { 
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<CreatorsInfo>(crinfo =>
        {
            crinfo.ToTable("CreatorsInfo");
            crinfo.HasKey(x => x.Creator).HasName("Creator");
            crinfo.Property(x => x.Company);
            crinfo.Property(x => x.Email);
            crinfo.Property(x => x.UserNumbers);
            crinfo.Property(x => x.CanBeCheckPoliceFines);
            crinfo
                .HasMany(x => x.UserCarInformation)
                .WithOne(u=>u.CreatorsInfo)
                .HasForeignKey(i=>i.CompanyId);
        });
    }
    public DbSet<UserCarInformation> UserCarInformation { get; set; }
    public DbSet<ReceivedSms> ReceivedSms { get; set; }
    public DbSet<CreatorsInfo> CreatorsInfo { get; set; }
}
