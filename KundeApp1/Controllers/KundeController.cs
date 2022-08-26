using System;
using System.Collections.Generic;
using KundeApp1.Models;
using Microsoft.AspNetCore.Mvc;

namespace KundeApp1.Controllers
{
    [Route("[controller]/[action]")]
    public class KundeController : ControllerBase
    {
        public List<Kunde> HentAlle()
        {
            var kundene = new List<Kunde>();

            var kunde1 = new Kunde();
            kunde1.navn = "Per Hansen";
            kunde1.adresse = "Ullevålsveien 1";

            var kunde2 = new Kunde
            {
                navn = "line Hansen",
                adresse = "Gabels gata 11"
            };

            kundene.Add(kunde1);
            kundene.Add(kunde2);

            return kundene;
        }
    }
}

