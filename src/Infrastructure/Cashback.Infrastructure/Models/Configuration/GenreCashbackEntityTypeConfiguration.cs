using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Infrastructure.Data.Models.Configuration
{
    public class GenreCashbackEntityTypeConfiguration : IEntityTypeConfiguration<GenreCashback>
    {
        public void Configure(EntityTypeBuilder<GenreCashback> builder)
        {
            builder.ToTable("GenreCashback")
                .HasKey(x => x.Id);
            builder.Property(x => x.GenreId)
                .IsRequired();
            builder.Property(x => x.Percent)
                .IsRequired()
                .HasDefaultValue(0);
            builder.HasOne(x => x.Genre);
        }
    }
}
