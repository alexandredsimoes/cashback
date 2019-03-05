using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Models.Configuration
{
    public class ServiceInfoEntityTypeConfiguration : IEntityTypeConfiguration<ServiceInfo>
    {
        public void Configure(EntityTypeBuilder<ServiceInfo> builder)
        {
            builder.ToTable("ServiceInfo")
                .HasKey(x => x.Id);
            builder.Property(x => x.AccessToken);
            builder.Property(x => x.ExpiresInMinutes)
                .IsRequired()
                .HasDefaultValue(0);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.RefreshToken)
                .IsRequired();
        }
    }
}
