using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Models.Configuration
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem")
                .HasKey(x => x.Id);
            builder.Property(x => x.AlbumId)
                .IsRequired();
            builder.Property(x => x.CashbackPercent)
                .IsRequired()
                .HasDefaultValue(0);
            builder.Property(x => x.CashBackTotal)
                .IsRequired()
                .HasDefaultValue(0);
            builder.Property(x => x.Quantity)
                .IsRequired()
                .HasDefaultValue(0);
            builder.Property(x => x.UnityPrice)
                .IsRequired()
                .HasDefaultValue(0);
            builder.HasOne(f => f.Order)
                .WithMany(x=>x.Items);
        }
    }
}
