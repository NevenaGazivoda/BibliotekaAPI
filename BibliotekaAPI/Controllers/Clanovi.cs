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
    [RoutePrefix("api/Clanovi")]
    public class ClanoviController : ApiController
    {   
        string connectionString;
        SqlConnection db;

        public ClanoviController()
        {
            connectionString = Connection.conStr;
            db = new SqlConnection(connectionString);
        }


        [Route("GET")]
        [HttpGet]
        public List<Clan> Citanje()
        {
            List<Clan> cList = new List<Clan>();
            
            //string queryString =
            //    "SELECT * from Clanovi";

            SqlCommand command = new SqlCommand("getAllFromClanovi", db)
            {
                CommandType = CommandType.StoredProcedure
            };
          
            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                

                while (reader.Read())
                {
                    Clan clan = new Clan();
                    clan.PKClanID = Convert.ToInt32(reader[0]);
                    clan.Ime = Convert.ToString(reader[1]);
                    clan.Prezime = Convert.ToString(reader[2]);
                    clan.GodRodjenja = Convert.ToInt32(reader[3]);

                    cList.Add(clan);
                }
                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cList;
        }

        [Route("GET/{idClana}")]
        [HttpGet]
        public Clan Citanje(int idClana)
        {

            //string queryString =
            //    "SELECT * from Clanovi where PKClanID="+idClana;

            SqlCommand command = new SqlCommand("getClanById", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@ClanId", SqlDbType.Int).Value = idClana;

            Clan clan = new Clan();
            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    clan.PKClanID = Convert.ToInt32(reader[0]);
                    clan.Ime = Convert.ToString(reader[1]);
                    clan.Prezime = Convert.ToString(reader[2]);
                    clan.GodRodjenja = Convert.ToInt32(reader[3]);

                }
                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return clan;
        }

        [Route("POST")]
        [HttpPost]
        public void Upis(Clan c)
        {
            string queryString =
                "INSERT INTO Clanovi (Ime, Prezime, GodRodjenja) VALUES ('" + c.Ime + "', '" + c.Prezime + "'," + c.GodRodjenja + ")";

            Console.WriteLine(queryString);

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