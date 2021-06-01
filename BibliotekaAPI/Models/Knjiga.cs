using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibliotekaAPI.Models
{
    public class Knjiga
    {
        public int PKKnjigaID { get; set; }
        public string Naziv { get; set; }
        public int GodIzdanja { get; set; }
        public int FKPisacID { get; set; }

        public string ImePisca { get; set; }
        public string PrezimePisca { get; set; }

        
    }
}