using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibliotekaAPI.Models
{
    public class Posudjivanje
    {
        public int FKClanID { get; set; }
        public int FKKnjigaID { get; set; }
        public DateTime DatumUzimanja { get; set; }
        public DateTime? DatumVracanja { get; set; }

        public string ImeClana { get; set; }
        public string PrezimeClana { get; set; }
        public string NazivKnjige { get; set; }
    }
}