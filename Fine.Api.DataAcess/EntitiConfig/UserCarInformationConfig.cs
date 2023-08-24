using Fine.Api.DataAccess.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fine.Api.DataAcess.EntitiConfig
{
    public class UserCarInformationConfig:IEntityTypeConfiguration<UserCarInformation>
    {
        public void Configure(EntityTypeBuilder<UserCarInformation> builder)
        {
            builder.ToTable("UserCarInformation");

            builder.HasKey(k => k.Id);

            builder
                .Property(c => c.CarNumber)
                .HasColumnType("Nvarchar(50)")
                .IsRequired();

            builder
                .Property(c => c.TechPassportId)
                .HasColumnType("Nvarchar(50)")
                .IsRequired();
        }
    }
}
