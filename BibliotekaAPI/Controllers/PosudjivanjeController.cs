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
            SqlCommand command = new SqlCommand("postPosudjivanje", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@FKClana", SqlDbType.Int).Value = pos.FKClanID;
            command.Parameters.Add("@FKKnjige", SqlDbType.Int).Value = pos.FKKnjigaID;
            command.Parameters.Add("@Datum", SqlDbType.Date).Value = DateTime.Now;
            

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