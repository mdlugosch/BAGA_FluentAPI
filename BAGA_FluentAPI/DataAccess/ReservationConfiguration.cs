using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /*
     * Reservation wird nur durch diese Konfigurationsdatei in das Model aufgenommen.
     * Im Context wird der ModelBuilder darauf eingestellt. Es wird jedoch kein DBset
     * für Reservation angelegt.
     */
    class ReservationConfiguration : EntityTypeConfiguration<Reservation> { }
}
