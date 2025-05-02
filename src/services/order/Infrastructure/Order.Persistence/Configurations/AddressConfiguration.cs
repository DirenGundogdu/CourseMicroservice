using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder) {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.Province).IsRequired().HasMaxLength(100);
        builder.Property(x => x.District).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Street).IsRequired().HasMaxLength(200);
        builder.Property(x => x.ZipCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Line).IsRequired().HasMaxLength(500);
        
    }
}