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
        
        modelBuilder.Entity<Logs>(logs =>
        {
            logs.ToTable("Logs");
            logs.HasKey(x => x.Id);
            logs.Property(x => x.Errors);
            logs.Property(x => x.ErrorTime);
        });
        
        modelBuilder.Entity<UserCarInformation>(uscar =>
        {
            uscar.ToTable("UserCarInformation");
            uscar.HasKey(x => x.Id);
            uscar
                .HasMany(x => x.Logs)
                .WithOne(u=>u.UserCarInformation)
                .HasForeignKey(i=>i.UserCarInformationId);
        });
    }
    public DbSet<UserCarInformation> UserCarInformation { get; set; }
    public DbSet<ReceivedSms> ReceivedSms { get; set; }
    public DbSet<CreatorsInfo> CreatorsInfo { get; set; }
    public DbSet<Logs> Logs { get; set; }
}
