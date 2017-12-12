using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class TripConfiguration : EntityTypeConfiguration<Trip>
    {
       public TripConfiguration()
        {
            // Das Attribut Identifier als Primärschlüssel setzen.
            HasKey(t => t.Identifier);
            // RowVersion-Attribut als TimeStamp konfigurieren.
            Property(t => t.RowVersion).IsRowVersion();
        }
    }
}
