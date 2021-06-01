using BibliotekaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BibliotekaAPI.Controllers
{
    [RoutePrefix("api/Pisci")]
    public class PisciController : ApiController
    {
        string connectionString;
        SqlConnection db;

        public PisciController()
        {
            connectionString = Connection.conStr;
            db = new SqlConnection(connectionString);
        }


        [Route("GET")]
        [HttpGet]
        public List<Pisac> Citanje()
        {
            string queryString =
               "SELECT * from Pisci";

            SqlCommand command = new SqlCommand(queryString, db);

            List<Pisac> pList = new List<Pisac>();

            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Pisac pisac = new Pisac();
                    pisac.PKPisacID = Convert.ToInt32(reader[0]);
                    pisac.Ime = Convert.ToString(reader[1]);
                    pisac.Prezime = Convert.ToString(reader[2]);
                    pisac.GodRodjenja = Convert.ToInt32(reader[3]);

                    pList.Add(pisac);
                }

                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return pList;
        }

        [Route("GET/{idPisca}")]
        [HttpGet]
        public Pisac CitanjePojedinacno(int idPisca)
        {
            string queryString =
               "SELECT * from Pisci where PKPisacID=" + idPisca;

            SqlCommand command = new SqlCommand(queryString, db);
            Pisac pisac = new Pisac();

            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pisac.PKPisacID = Convert.ToInt32(reader[0]);
                    pisac.Ime = Convert.ToString(reader[1]);
                    pisac.Prezime = Convert.ToString(reader[2]);
                    pisac.GodRodjenja = Convert.ToInt32(reader[3]);
                    
                }

                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return pisac;
        }

        [Route("POST")]
        [HttpPost]
        public void Unos(Pisac p)
        {
            string queryString = "INSERT INTO Pisci (Ime, Prezime, GodRodjenja) " +
               "VALUES ('" + p.Ime + "' ,'" + p.Prezime + "'," + p.GodRodjenja + ")";

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

        [Route("PUT")]
        [HttpPut]
        public void Izmjena(Pisac p)
        {
            string queryString = "UPDATE Pisci SET [Ime] = '" +p.Ime+
                "',[Prezime] ='" + p.Prezime +
                "',[GodRodjenja] =" + p.GodRodjenja +
                " WHERE PKPisacID="+p.PKPisacID;
              
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