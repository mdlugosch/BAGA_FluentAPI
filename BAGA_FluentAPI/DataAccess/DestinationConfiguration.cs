using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess
{
    /*
     * Konfiguration der Modelklassen über Fluent-API und eigene Konfigurationsklassen.
     * Bei umfangreichen Klassen ist dies sinnvoll.
     */ 
    class DestinationConfiguration : EntityTypeConfiguration<Destination>
    {
        public DestinationConfiguration()
        {
            ToTable("Locations", "baga");
            Property(d => d.Name).IsRequired().HasColumnName("LocationName");
            Property(d => d.DestinationId).HasColumnName("LocationID");
            Property(d => d.Description).HasMaxLength(500);
            Property(d => d.Photo).HasColumnType("image");
            //Map(m =>
            //{
            //    m.Properties(d => new { d.Name, d.Country, d.Description });
            //    m.ToTable("Locations");
            //});
            //Map(m =>
            //{
            //    m.Properties(d => new { d.Photo });
            //    m.ToTable("LocationPhotos");
            //});
        }
    }
}
