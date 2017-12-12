using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess
{
    class LodgingConfiguration : EntityTypeConfiguration<Lodging>
    {
        public LodgingConfiguration()
        {
            Property(l => l.Name).IsRequired().HasMaxLength(200);
            Property(l => l.MilesFromNearestAirport).HasPrecision(8, 1);

            Map(m => { m.ToTable("Lodging"); m.Requires("IsResort").HasValue(false); })
           .Map<Resort>(m => { m.Requires("IsResort").HasValue(true); });
        }
    }
}
