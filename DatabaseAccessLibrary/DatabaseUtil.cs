﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class DatabaseUtil
    {
        private SqlConnection conn;

        public void Setup()
        {
            // Instantiate the connection
           // conn = new SqlConnection("Data Source=i4dab.ase.au.dk;Initial Catalog=F17I4DABH2Gr18;User ID=F17I4DABH2Gr18; Password=F17I4DABH2Gr18");
            conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HandIn2DAB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public enum TABLE
        {
            Person,
            Adresse,
            TelefonNr
        }

        public int GetNumberOfRecords(TABLE t)
        {
            int count = -1;

            try
            {
                // Open connection
                conn.Open();

                // Instantiate a new command
                SqlCommand cmd = new SqlCommand("select count(*) from " + (t == TABLE.Person ? "Person" : (t == TABLE.Adresse ? "Adresse" : "TelefonNr")), conn);

                // Send command
                count = (int)cmd.ExecuteScalar();
            }
            finally
            {
                // Close connection
                    conn?.Close();
            }
            return count;
        }

        public PersonJoin GetAllRecordsForPerson(int personId)
        {
            var personJoin = new PersonJoin(GetPerson(personId), GetAllAdressesForPerson(personId), GetTelefons(personId));
             
            return personJoin;
        }

        public List<PersonModel> GetPersons(bool sortAsc = true)
        {
            SqlDataReader rdr = null;
            List<PersonModel> list = new List<PersonModel>();

            try
            {
                // Open connection
                conn.Open();

                string cmdString = "select Fornavn, MellemNavn, Efternavn, Type, AdresseId from Person ORDER BY Nummer #sort";
               
                // Instantiate a new command
                SqlCommand cmd = new SqlCommand(cmdString.Replace("#sort", (sortAsc ? "ASC" : "DSC")), conn);

                // Send command
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var person = new PersonModel(
                        Convert.ToInt32(rdr["PersonId"]),
                        rdr["Fornavn"].ToString(),
                        rdr["Mellemnavn"].ToString(),
                        rdr["Efternavn"].ToString(),
                        rdr["Type"].ToString(),
                        Convert.ToInt32(rdr["AdresseId"])
                    );
                    
                    list.Add(person);
                }
            }
            finally
            {
                // Close connection
                    rdr?.Close();
                    conn?.Close();
            }

            return list;
        }

        public int SetPersonName(string fornavn, int personId)
        {

            try
            {
                conn.Open();
                string cmdString = "UPDATE Person SET Fornavn='#name' WHERE PersonId='#personId'";
                cmdString = cmdString.Replace("#name", fornavn);
                SqlCommand sqlCommand = new SqlCommand(cmdString.Replace("#personId",personId.ToString()), conn);
                return sqlCommand.ExecuteNonQuery();

            }
            finally 
            {
                conn?.Close();
            }
        }

        public void DeletePerson(int personId )
        {
            List<AdresseModel> list;
            try
            {

                list = GetAllAdressesForPerson(personId);
                SqlCommand sqlCommand = null;
                sqlCommand = new SqlCommand("DELETE FROM Person WHERE PersonId = " + personId, conn);
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                foreach (var item in list)
                {
                    sqlCommand =
                        new SqlCommand("SELECT count(*) From HarAdresse Where HarAdresse.AdresseId = '" + item.AdresseId+"'",conn);
                    if ( (int)sqlCommand.ExecuteScalar() == 0)
                    {
                        SqlCommand sqlDeleteCommand =
                            new SqlCommand("DELETE FROM Adresse WHERE AdresseId = "+item.AdresseId, conn);
                        sqlDeleteCommand.ExecuteNonQuery();

                    }
                }


            }
            finally
            { 
                conn?.Close();
            }

       }

        public int InsertPerson(int personId, string navn,string efternavn, string mellemnavn,string type,int AdresseId)
        {
            SqlDataReader rdr = null;
            string comRoot;
            if (mellemnavn.Length == 0)
            {
                comRoot = "INSERT INTO Person(PersonId,Fornavn,Efternavn,Type,AdresseId) VALUES ";
                comRoot += "(" + personId + ",'" + navn + "','" + efternavn + "','" + type + "'," + AdresseId + ")";
            }
            else
            {
                comRoot = "INSERT INTO Person(PersonId,Fornavn,Efternavn,Mellemnavn,Type,AdresseId) VALUES ";
                comRoot += "(" + personId + ",'" + navn + "','" + efternavn + "','"+mellemnavn+"','" + type + "'," + AdresseId + ")";
            }

            try
            {
                SqlCommand sqc = null;
                sqc = new SqlCommand(comRoot, conn); 
                conn.Open();
                return sqc.ExecuteNonQuery();

            }
            finally
            {
                conn?.Close();
                
            }
        }

        public PersonModel GetPerson(int personId)
        {
            SqlDataReader rdr = null;
            PersonModel pm;

            try
            {
                // Open connection
                conn.Open();
              
                string cmdString = "select * from Person where (PersonId = #personId)";

                // Instantiate a new command
                SqlCommand cmd = new SqlCommand(cmdString.Replace("#personId", personId.ToString()), conn);

                // Send command
                rdr = cmd.ExecuteReader();

                rdr.Read();

                pm = new PersonModel(personId, 
                    rdr["Fornavn"].ToString(), 
                    rdr["Mellemnavn"].ToString(), 
                    rdr["Efternavn"].ToString(), 
                    rdr["Type"].ToString(),
                    Convert.ToInt32(rdr["AdresseId"]));


            }
            finally
            {
                // Close connection
                    rdr?.Close();
                    conn?.Close();
            }

            return pm;
        }

        public AdresseModel GetHomeAdresse(int addressId)
        {
            SqlDataReader sqlDataReader = null;
            AdresseModel adresseModel = null;

            try
            {
                string command = "select * from Adresse where AdresseId = #adresseId";
                SqlCommand cmd = new SqlCommand(command.Replace("#adresseId",addressId.ToString()),conn);
                conn.Open();
                sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    adresseModel = new AdresseModel(
                        Convert.ToInt32(sqlDataReader["AdresseId"]),
                        sqlDataReader["Vejnavn"].ToString(),
                        Convert.ToInt32(sqlDataReader["HusNr"]),
                        Convert.ToInt32(sqlDataReader["PostNr"]),
                        sqlDataReader["Bynavn"].ToString(),
                        sqlDataReader["Type"].ToString());

                }
            }
            finally 
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
                if (conn != null)
                    conn.Close();
            }
            return adresseModel;
        }

        public List<AdresseModel> GetAllAdressesForPerson(int personId)
        {
            SqlDataReader rdr = null;
            List<AdresseModel> list = new List<AdresseModel>();
            SqlConnection secondConn = new SqlConnection(conn.ConnectionString); //nød til at have en ekstra, da conn bliver åbnet i getHomeAdress
            try
            {
                string cmdString = "select * from HarAdresse where (PersonId = #personId)";


                cmdString = cmdString.Replace("#personId", personId.ToString());

                SqlCommand cmd = new SqlCommand(cmdString, secondConn);

                secondConn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    AdresseModel adresse = GetHomeAdresse(Convert.ToInt32(rdr["AdresseId"]));

                    list.Add(adresse);
                }
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
                if (conn != null)
                    conn.Close();
                if(secondConn != null)
                    secondConn.Close();
            }
            return list;

        }

        public List<AdresseModel> GetAdresses()
        {
            SqlDataReader sqlDataReader = null;
            List<AdresseModel> list = new List<AdresseModel>();
            try
            {
              
                SqlCommand cmd = new SqlCommand("select * from Adresse", conn);
                conn.Open();
                sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    AdresseModel adresse = new AdresseModel(
                        Convert.ToInt32(sqlDataReader["AdresseId"]),
                        sqlDataReader["Vejnavn"].ToString(),
                        Convert.ToInt32(sqlDataReader["HusNr"]),
                        Convert.ToInt32(sqlDataReader["PostNr"]),
                        sqlDataReader["Bynavn"].ToString(),
                        sqlDataReader["Type"].ToString());

                    list.Add(adresse);
                }
            }
            finally
            {
                if(sqlDataReader != null)
                    sqlDataReader.Close();
                if(conn != null)
                    conn.Close();
            }
            return list;

        }

        public List<TelefonModel> GetTelefons(int personId)
        {
            SqlDataReader rdr = null;
            List<TelefonModel> list = new List<TelefonModel>();
            try
            {
                // Open connection
                conn.Open();

                string cmdString = "select * from TelefonNr where (PersonId = #personId)";

                // Instantiate a new command
                SqlCommand cmd = new SqlCommand(cmdString.Replace("#personId", personId.ToString()), conn);

                // Send command
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    TelefonModel phone = new TelefonModel(
                    Convert.ToInt32(rdr["TelefonId"]),
                    rdr["Type"].ToString(),
                    rdr["Nummer"].ToString(),
                   Convert.ToInt32(rdr["PersonId"]));

                    list.Add(phone);
                }
            }
            finally
            {
                // Close connection
                rdr?.Close();
                conn?.Close();
            }

            return list;
        }

        public TelefonModel GetHomeTelefon(int personId)
        {
            SqlDataReader rdr = null;
            TelefonModel phone;
            try
            {
                // Open connection
                conn.Open();

                string cmdString = "select * from TelefonNr where (PersonId = #personId) AND (Type = 'Hjem')";

                // Instantiate a new command
                SqlCommand cmd = new SqlCommand(cmdString.Replace("#personId", personId.ToString()), conn);

                // Send command
                rdr = cmd.ExecuteReader();

                rdr.Read();
                phone = new TelefonModel(
                Convert.ToInt32(rdr["TelefonId"]),
                rdr["Type"].ToString(),
                rdr["Nummer"].ToString(),
               Convert.ToInt32(rdr["PersonId"]));
            }
            finally
            {
                // Close connection
                rdr?.Close();
                conn?.Close();
            }

            return phone;
        }
    }
}

