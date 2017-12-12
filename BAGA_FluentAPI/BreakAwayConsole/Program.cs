using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccess;
using System.Data.Entity;
using System.Diagnostics;

namespace BreakAwayConsole
{
    public class Program
    {
        private static void InsertDestination()
        {
            var destination = new Destination()
            {
                Country = "Indonesia",
                Description = "EcoTourism at its best in exquisite Bali",
                Name = "Bali"
            };

            using (var context = new BreakAwayContext())
            {
                context.Destinations.Add(destination);
                context.SaveChanges();
            }
        }

        public static void ShowDestination()
        {
            using (var context = new BreakAwayContext())
            {
                var query = from row in context.Destinations
                            select row;

                foreach (var element in query)
                {
                    Console.WriteLine(element.DestinationId + " / " + element.Name + " / " + element.Description);
                }
            }
        }

        public static void ShowTrip()
        {
            using (var context = new BreakAwayContext())
            {
                var query = from row in context.Trips
                            select row;

                foreach (var element in query)
                {
                    Console.WriteLine(element.Identifier + " / " + element.StartDate + " / " + element.EndDate + " / " + element.CostUSD);
                    foreach (int i in element.RowVersion) { Console.Write(i); }
                    Console.WriteLine();
                }
            }
        }

        public static void ClearDestinations()
        {
            using (var context = new BreakAwayContext())
            {
                var query = from row in context.Destinations
                            where row.DestinationId > 0
                            select row;

                foreach (var element in query)
                {
                    context.Destinations.Remove(element);
                }
                context.SaveChanges();
            }
        }

        private static void InsertTrip()
        {
            var trip = new Trip
            {
                CostUSD = 800,
                StartDate = new DateTime(2011, 9, 1),
                EndDate = new DateTime(2011, 9, 14)
            };

            using (var context = new BreakAwayContext())
            {
                context.Trips.Add(trip);
                context.SaveChanges();
            }
        }

        public static void UpdateTrip(int usd)
        {
            using (var context = new BreakAwayContext())
            {
                var trip = context.Trips.FirstOrDefault();
                trip.CostUSD = usd;
                context.SaveChanges();
            }
        }

        private static void InsertPerson()
        {
            var person = new Person
            {
                FirstName = "Rowan",
                LastName = "Miller",
                SocialSecurityNumber = 12345678,
                Photo = new PersonPhoto { Photo = new Byte[] { 0 } }
            };

            using (var context = new BreakAwayContext())
            {
                context.People.Add(person);
                context.SaveChanges();
            }
        }

        public static void UpdatePerson(string firstname)
        {
            using (var context = new BreakAwayContext())
            {
                var person = context.People.FirstOrDefault();
                Console.WriteLine("<Taste Drücken um fortzufahren>");
                Console.ReadKey();
                person.FirstName = firstname;
                if (person.Photo == null)
                {
                    person.Photo = new PersonPhoto { Photo = new Byte[] { 0 } };
                }
                context.SaveChanges();
            }
        }

        public static void UpdateSocialSecurityNumber(int ssn)
        {
            using (var context = new BreakAwayContext())
            {
                var person = context.People.FirstOrDefault();
                person.SocialSecurityNumber = ssn;
                context.SaveChanges();
            }
        }

        public static void ShowPeople()
        {
            using (var context = new BreakAwayContext())
            {
                var query = from row in context.People
                            select row;

                foreach (var element in query)
                {
                    Console.WriteLine(element.SocialSecurityNumber + " / " + element.FirstName + " / " + element.LastName);
                }
            }
        }

        private static void DeleteDestinationInMemoryAndDbCascade()
        {
            int destinationId;
            using (var context = new BreakAwayContext())
            {
                var destination = new Destination
                {
                    Name = "Sample Destination",
                    Lodgings = new List<Lodging>
                    {
                        new Lodging { Name="Lodging One" },
                        new Lodging { Name="Lodging Two" }
                    }
                };

                context.Destinations.Add(destination);
                context.SaveChanges();
                destinationId = destination.DestinationId;
            }

            using (var context = new BreakAwayContext())
            {
                var destination = context.Destinations
                    .Include("Lodgings")
                    .Single(d => d.DestinationId == destinationId);

                var aLodging = destination.Lodgings.FirstOrDefault();
                context.Destinations.Remove(destination);

                Console.WriteLine("State of one Lodging: {0}", context.Entry(aLodging).State.ToString());

                context.SaveChanges();
            }
            }

        private static void ShowLodgings()
        {
            using (var context = new BreakAwayContext())
            {
                var query = from row in context.Lodgings
                            select row;

                foreach (var element in query)
                {
                    Console.WriteLine(element.Name + " / " + element.MilesFromNearestAirport);
                }

                Debug.WriteLine(context.Database.Connection.ConnectionString);
            };
        }

        /*
         * Hier wird das Löschen aufgerufen ohne vorher alle Daten in den
         * Speicher zu holen. Wirkung: Verweiste Objekte werden wegen der
         * Cascadierungsregel von EF gelöscht.
         */
        private static void DeleteDestinationInMemoryAndDbCascade2()
        {
            int destinationId;
            using (var context = new BreakAwayContext())
            {
                var destination = new Destination
                {
                    Name = "Sample Destination",
                    Lodgings = new List<Lodging>
                    {
                        new Lodging { Name="Lodging One" },
                        new Lodging { Name="Lodging Two" }
                    }
                };

                context.Destinations.Add(destination);
                context.SaveChanges();
                destinationId = destination.DestinationId;
            }

            using (var context = new BreakAwayContext())
            {
                var destination = context.Destinations
                    .Single(d => d.DestinationId == destinationId);

                context.Destinations.Remove(destination);
                context.SaveChanges();
            }

            using (var context = new BreakAwayContext())
            {
                var lodgings = context.Lodgings
                    .Where(l => l.DestinationId == destinationId).ToList();
                Console.WriteLine("Lodgings: {0}", lodgings.Count);
            }
        }

        private static void InsertLodging()
        {
            var lodging = new Lodging
            {
                Name = "Rainy Day Motel",
                Destination = new Destination
                {
                    Name = "Seattle, Washington",
                    Description = "Tourism at its best at Capital of the USA",
                    Country = "USA"
                }
            };

            using(var context = new BreakAwayContext())
            {
                context.Lodgings.Add(lodging);
                context.SaveChanges();
            }
        }

        public static void InsertResort()
        {
            var resort = new Resort
            {
                Name = "Top Notch Resort and Spa",
                MilesFromNearestAirport = 30,
                Activities = "Spa, Hiking, Skiing, Ballooning",
                Destination = new Destination
                {
                    Name = "Stowe, Vermont",
                    Country = "USA"
                }
            };

            using(var context = new BreakAwayContext())
            {
                context.Lodgings.Add(resort);
                context.SaveChanges();
            }
        }

        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BreakAwayContext>());
            //InsertTrip();
            //InsertPerson();
            //InsertDestination();
            //InsertLodging();
            //ClearDestinations();
            //InsertResort();
            
            Console.WriteLine("--------------------- Destinations --------------------");
            ShowDestination();
            Console.WriteLine("--------------------- Trips ---------------------------");
            ShowTrip();
            Console.WriteLine("--------------------- Edited Trips -------------------");
            UpdateTrip(500);
            ShowTrip();
            Console.WriteLine("--------------------- Lodgings -----------------------");
            ShowLodgings();
            Console.WriteLine("--------------------- People --------------------------");
            ShowPeople();
            Console.WriteLine("----------------- Cascade Delete ----------------------");
            DeleteDestinationInMemoryAndDbCascade2();
            Console.ReadKey();
        }
    }
}
