using BibliotekaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BibliotekaAPI.Controllers
{
    [RoutePrefix("api/Posudjivanje")]
    public class PosudjivanjeController : ApiController
    {
        string connectionString;
        SqlConnection db;

        public PosudjivanjeController()
        {
            connectionString = Connection.conStr;
            db = new SqlConnection(connectionString);
        }


        [Route("GET")]
        [HttpGet]
        public List<Posudjivanje> Citanje()
        {
            //string queryString =
            //    "select Clanovi.Ime, Clanovi.Prezime, " +
            //    "Knjige.Naziv, Posudjivanje.DatumUzimanja, Posudjivanje.DatumVracanja from Posudjivanje join Clanovi on Posudjivanje.FKClanID = Clanovi.PKClanID join Knjige on Posudjivanje.FKKnjigaID = Knjige.PKKnjigaID";

            SqlCommand command = new SqlCommand("getAllFromPosudjivanje", db)
            {
                CommandType = CommandType.StoredProcedure
            };
           

            List<Posudjivanje> posList = new List<Posudjivanje>();
            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Posudjivanje posudjivanje = new Posudjivanje();

                    posudjivanje.ImeClana = Convert.ToString(reader[0]);
                    posudjivanje.PrezimeClana = Convert.ToString(reader[1]);
                    posudjivanje.NazivKnjige = Convert.ToString(reader[2]);
                    posudjivanje.DatumUzimanja = Convert.ToDateTime(reader[3]);
                    //posudjivanje.DatumVracanja = Convert.ToDateTime(reader[4]).Date;


                    if (reader[4] == DBNull.Value)
                    {
                        posudjivanje.DatumVracanja = null;
                    }
                    else
                    {
                        posudjivanje.DatumVracanja = Convert.ToDateTime(reader[4]);
                    }

                    posList.Add(posudjivanje);
                }
                
                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return posList;
        }
        
        [Route("POST")]
        [HttpPost]
        public void Unos(Posudjivanje pos)
        {
            string queryString = "INSERT INTO Posudjivanje (FKClanID, FKKnjigaID, DatumUzimanja) " +
                "VALUES (" + pos.FKClanID + " ," + pos.FKKnjigaID + ",'" + DateTime.Now + "')";

            SqlCommand command = new SqlCommand(queryString, db);

            try
            {
                db.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            db.Close();
        }
    }
}