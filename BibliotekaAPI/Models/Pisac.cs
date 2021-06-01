using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibliotekaAPI.Models
{
    public class Pisac
    {
        public int PKPisacID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int GodRodjenja { get; set; }
    }
}