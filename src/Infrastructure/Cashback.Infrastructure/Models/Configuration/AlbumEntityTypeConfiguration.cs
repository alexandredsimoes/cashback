using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Models.Configuration
{
    public class AlbumEntityTypeConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Album")
                .HasKey(x => x.Id);
            builder.Property(x => x.AlbumName)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(x => x.GenreId)
                .IsRequired();
            builder.HasOne(f => f.Genre);
            builder.Property(x => x.Identifier)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(x => x.Price)
                .IsRequired()
                .HasDefaultValue(0);
        }
    }
}
