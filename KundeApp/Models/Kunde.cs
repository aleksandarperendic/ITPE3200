using System;
using System.Collections.Generic;

namespace KundeApp.Models
{
    public class Kunde
    {
        public int Id { get; set; } // med Id som variabel blir dette en autoincrement i databasen (pr. default).
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public string Poststed { get; set; }

    }
}
