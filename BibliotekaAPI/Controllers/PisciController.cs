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
            SqlCommand command = new SqlCommand("getAllFromPisci", db)
            {
                CommandType = CommandType.StoredProcedure
            };

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
            //string queryString =
            //   "SELECT * from Pisci where PKPisacID=" + idPisca;

            SqlCommand command = new SqlCommand("getPisacById", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@PisacId", SqlDbType.Int).Value = idPisca;

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
            SqlCommand command = new SqlCommand("postPisci", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Ime", SqlDbType.VarChar).Value = p.Ime;
            command.Parameters.Add("@Prezime", SqlDbType.VarChar).Value = p.Prezime;
            command.Parameters.Add("@Godina", SqlDbType.Int).Value = p.GodRodjenja;


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
            SqlCommand command = new SqlCommand("updatePisci", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Ime", SqlDbType.VarChar).Value = p.Ime;
            command.Parameters.Add("@Prezime", SqlDbType.VarChar).Value = p.Prezime;
            command.Parameters.Add("@Godina", SqlDbType.Int).Value = p.GodRodjenja;
            command.Parameters.Add("@PisacId", SqlDbType.Int).Value = p.PKPisacID;


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

        [Route("DELETE/{idPisca}")]
        [HttpDelete]
        public void Brisanje(int idPisca)
        {
            SqlCommand command = new SqlCommand("deletePisci", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@IdPisca", SqlDbType.Int).Value = idPisca;

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