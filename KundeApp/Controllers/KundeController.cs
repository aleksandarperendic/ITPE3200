using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KundeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KundeApp.Controllers
{
    [Route("[controller]/[action]")]
    public class KundeController : ControllerBase
    {
        private readonly KundeContext _db;

        public KundeController(KundeContext db)
        {
            _db = db;
        }

        public async Task<bool> Opprett(Kunde innKunde)
        {
            try
            {
                var nyKundeRad = new Kunder();
                nyKundeRad.Fornavn = innKunde.Fornavn;
                nyKundeRad.Etternavn = innKunde.Etternavn;
                nyKundeRad.Adresse = innKunde.Adresse;

                var sjekkPostnr = await _db.Poststeder.FindAsync(innKunde.Postnr);
                if (sjekkPostnr == null)
                {
                    var poststedsRad = new Poststeder();
                    poststedsRad.Postnr = innKunde.Postnr;
                    poststedsRad.Poststed = innKunde.Poststed;
                    nyKundeRad.Poststed = poststedsRad;
                }
                else
                {
                    nyKundeRad.Poststed = sjekkPostnr;
                }
                _db.Kunder.Add(nyKundeRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<Kunde>> HentAlle()
        {
            try {
                List<Kunde> alleKunder = await _db.Kunder.Select(k => new Kunde
                {
                    Id = k.Id,
                    Fornavn = k.Fornavn,
                    Etternavn = k.Etternavn,
                    Adresse = k.Adresse,
                    Postnr = k.Poststed.Postnr,
                    Poststed = k.Poststed.Poststed
                }).ToListAsync();
                return alleKunder;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Slett(int id)
        {
            try
            {
                Kunder enDbKunde = await _db.Kunder.FindAsync(id);
                _db.Kunder.Remove(enDbKunde);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Kunde> HentEn(int id)
        {
            try
            {
                Kunder enKunde = await _db.Kunder.FindAsync(id);
                var hentetKunde = new Kunde()
                {
                    Id = enKunde.Id,
                    Fornavn = enKunde.Fornavn,
                    Etternavn = enKunde.Etternavn,
                    Adresse = enKunde.Adresse,
                    Postnr = enKunde.Poststed.Postnr,
                    Poststed = enKunde.Poststed.Poststed
                };
                return hentetKunde;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> Endre(Kunde endreKunde)
        {
            try
            {
                var endreKundeObjekt = await _db.Kunder.FindAsync(endreKunde.Id);
                if (endreKundeObjekt.Poststed.Postnr != endreKunde.Postnr)
                {
                    var sjekkPoststed = _db.Poststeder.Find(endreKunde.Postnr);
                    if (sjekkPoststed == null)
                    {
                        var poststedsRad = new Poststeder();
                        poststedsRad.Postnr = endreKunde.Postnr;
                        poststedsRad.Poststed = endreKunde.Poststed;
                        endreKundeObjekt.Poststed = poststedsRad;
                    }
                    else
                    {
                        endreKundeObjekt.Poststed.Postnr = endreKunde.Postnr;
                    }
                }

                endreKundeObjekt.Fornavn = endreKunde.Fornavn;
                endreKundeObjekt.Etternavn = endreKunde.Etternavn;
                endreKundeObjekt.Adresse = endreKunde.Adresse;
                await _db.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
