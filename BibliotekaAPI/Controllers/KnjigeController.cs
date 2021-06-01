﻿using BibliotekaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BibliotekaAPI.Controllers
{
    [RoutePrefix("api/Knjige")]
    public class KnjigeController : ApiController
    {
        string connectionString;
        SqlConnection db;

        public KnjigeController()
        {
            connectionString = Connection.conStr;
            db = new SqlConnection(connectionString);
        }


        [Route("GET")]
        [HttpGet]
        public List<Knjiga> hxjhkghfjhk()
        {
            string queryString =
               "select k.PKKnjigaID, k.Naziv, p.Ime, p.Prezime " +
               "from knjige as k join Pisci as p " +
               "on k.FKPisacID = p.PKPisacID";

            SqlCommand command = new SqlCommand(queryString, db);
            List<Knjiga> kList = new List<Knjiga>();

            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Knjiga knjiga = new Knjiga();

                    knjiga.PKKnjigaID = Convert.ToInt32(reader[0]);
                    knjiga.Naziv = Convert.ToString(reader[1]);

                    knjiga.ImePisca = Convert.ToString(reader[2]);
                    knjiga.PrezimePisca = Convert.ToString(reader[3]);

                    kList.Add(knjiga);
                }
                

                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return kList;
        }

        [Route("POST")]
        [HttpPost]
        public void Upis(Knjiga k)
        {
            string queryString = "INSERT INTO[dbo].[Knjige] ([Naziv] ,[GodIzdanja] ,[FKPisacID]) " +
                "VALUES ('" + k.Naziv + "' ," + k.GodIzdanja + "," + k.FKPisacID + ")";


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