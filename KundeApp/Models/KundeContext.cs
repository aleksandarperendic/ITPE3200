using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KundeApp.Models
{
    public class Kunder
    {
        public int Id { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Adresse { get; set; }

        // låner verdier fra andre klasser med 'virtual'
        virtual public Poststeder Poststed { get; set; }
    }

    public class Poststeder
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Postnr { get; set; }
        public string Poststed { get; set; }

        // denne listen ikke nødvendig med mindre man skal finne kundene på et gitt postnr (altså gå inn via Poststeder-collection)
        // virtual public List<Kunder> Kunder { get; set; }
    }

    public class KundeContext : DbContext
    {
        public KundeContext(DbContextOptions<KundeContext> options) : base(options)
        {
            // denne brukes for å opprette databasen fysisk dersom den ikke er opprettet
            // dette er uavhenig av initiering av databasen (seeding)
            // når man endrer på strukturen på KundeContxt her er det fornuftlig å slette denne fysisk før nye kjøringer
            Database.EnsureCreated();
        }

        public DbSet<Kunder> Kunder { get; set; }
        public DbSet<Poststeder> Poststeder { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // må importere pakken Microsoft.EntityFrameworkCore.Proxies
            // og legge til"viritual" på de attriuttene som ønskes å lastes automatisk (LazyLoading)
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
