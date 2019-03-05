using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Models.Configuration
{    
    public class BasketEntityTypeConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.ToTable("Basket")
                .HasKey(x => x.Id);
            builder.Property(x => x.AlbumId)
                .IsRequired();
            builder.Property(x => x.CustomerId)
                .IsRequired();
            builder.Property(x => x.Quantity)
                .IsRequired().HasDefaultValue(1);
            builder.Property(x => x.UnitPrice)
                .IsRequired();
            builder.HasOne(x => x.Album);
            builder.HasOne(x => x.Customer);
        }
    }
}
