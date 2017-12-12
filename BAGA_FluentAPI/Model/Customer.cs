using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /*
     * Die Klasse Customer schränkt den Zugriff aus die Property Name ein.
     * Der Zugriff erfolgt über eine Methode. Name selbst ist private und
     * würde von EF nicht gemappt werden. Damit dies trotzdem geschiet muss
     * eine eingebettete Konfigurationsklasse geschrieben werden die trotzdem
     * für das Mapping der Property Name sorgt. Diese Klasse muss eingebettet
     * werden damit sie nur so Zugriff auf die Variable der Übergeordnaten
     * Klasse hat.
     */
    public class Customer
    {
        public int CustomerId { get; set; }
        private string Name { get; set; }

        public class CustomerConfig : EntityTypeConfiguration<Customer>
        {
            public CustomerConfig()
            {
                Property(b => b.Name);
            }
        }

        public string GetName()
        {
            return this.Name;
        }

        public static Customer CreateCustomer(string name)
        {
            return new Model.Customer { Name = name };
        }
    }
}
