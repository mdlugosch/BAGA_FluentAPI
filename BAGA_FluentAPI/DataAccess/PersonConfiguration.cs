using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            /*
             * SocialSecurityNumber war zuvor der Primärschlüssel und wurde durch PersonId ersetzt.
             * SocialSecurityNumber sollte die auswirkung von DatabaseGeneratedOption.None demonstrieren.
             */
            //HasKey(p => p.SocialSecurityNumber).Property(p => p.SocialSecurityNumber).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            /*
             * ConcurrencyToken für SocialSecurityNumber einstellen.
             */
            ToTable("People");
            Property(p => p.SocialSecurityNumber).IsConcurrencyToken();

            Property(p => p.Address.State).HasColumnName("State");
        }
    }
}
