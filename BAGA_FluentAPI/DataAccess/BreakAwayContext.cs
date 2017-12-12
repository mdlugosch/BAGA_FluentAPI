using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess
{
    public class BreakAwayContext : DbContext
    {
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Lodging> Lodgings { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Person> People { get; set; }

        /*
         * OnModelCreating ladet die Konfigurationsklassen und stellt damit
         * die Konfiguration der Attribute her.
         */
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DestinationConfiguration());
            modelBuilder.Configurations.Add(new LodgingConfiguration());
            // modelBuilder.Entity<Trip>().HasKey(t => t.Identifier);
            modelBuilder.Configurations.Add(new TripConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new PersonalInfoConfiguration());
            modelBuilder.Configurations.Add(new MeasurementConfiguration());
            modelBuilder.Configurations.Add(new ActivityConfiguration());
            modelBuilder.Configurations.Add(new PersonPhotoConfiguration());
            modelBuilder.Configurations.Add(new ReservationConfiguration());

            modelBuilder.Configurations.Add(new Customer.CustomerConfig());

            modelBuilder.Ignore<MyInMemoryOnlyClass>();

            // Address als ComplexType konfigurieren und die Property StreetAddress auf Max Length 150 stellen.
            modelBuilder.ComplexType<Address>()
                .Property(p => p.StreetAddress)
                .HasMaxLength(150)
                .HasColumnName("StreetAddress");

            //modelBuilder.ComplexType<Person>()
            //    .Property(p => p.Address.City)
            //    .HasColumnName("City");

            modelBuilder.ComplexType<PersonalInfo>();
            modelBuilder.ComplexType<Measurement>();

            modelBuilder.Entity<InternetSpecial>()
                .HasRequired(s => s.Accommodation)
                .WithMany(l => l.InternetSpecials)
                .HasForeignKey(s => s.AccommodationId);

            modelBuilder.Entity<Lodging>()
                        .HasOptional(s => s.PrimaryContact)
                        .WithMany(l => l.PrimaryContactFor);

            modelBuilder.Entity<Lodging>()
                        .HasOptional(s => s.SecondaryContact)
                        .WithMany(l => l.SecondaryContactFor);
           
        }
    }
}
