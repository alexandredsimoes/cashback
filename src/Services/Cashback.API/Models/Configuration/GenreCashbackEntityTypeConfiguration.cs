using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.API.Models.Configuration
{
    public class GenreCashbackEntityTypeConfiguration : IEntityTypeConfiguration<GenreCashback>
    {
        public void Configure(EntityTypeBuilder<GenreCashback> builder)
        {
            throw new NotImplementedException();
        }
    }
}
