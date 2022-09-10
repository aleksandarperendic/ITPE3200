using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace KundeApp.Models
{
    public class DBinit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<KundeContext>();

                // må slette og opprette databasen hver gang når den skal initieres (seed'es)
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var poststed1 = new Poststeder { Postnr = "0270", Poststed = "Oslo" };
                var poststed2 = new Poststeder { Postnr = "1370", Poststed = "Asker" };

                var kunde1 = new Kunder { Fornavn = "Ole", Etternavn = "Hansen", Adresse = "Osloveien 82", Poststed = poststed1 };
                var kunde2 = new Kunder { Fornavn = "Line", Etternavn = "Jensen", Adresse = "Askerveien 72", Poststed = poststed2 };

                context.Kunder.Add(kunde1);
                context.Kunder.Add(kunde2);

                context.SaveChanges();
            }
        }
    }
}
