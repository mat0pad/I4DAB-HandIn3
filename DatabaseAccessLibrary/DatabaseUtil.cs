using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
            //conn = new SqlConnection("Data Source=i4dab.ase.au.dk;Initial Catalog=F17I4DABH2Gr18;User ID=F17I4DABH2Gr18; Password=F17I4DABH2Gr18");
            conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HandIn2DAB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public int GetNumberOfRecords()
        {
            int count = -1;

            try
            {
                // Open connection
                conn.Open();

                // Instantiate a new command
                SqlCommand cmd = new SqlCommand("select count(*) from Person", conn);

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

        public void GetPersons()
        {
            SqlDataReader rdr = null;

            try
            {
                // Open connection
                conn.Open();

                // Instantiate a new command
                SqlCommand cmd = new SqlCommand("select Fornavn, MellemNavn, Efternavn, Type, AdresseId from Person", conn);

                // Send command
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] + "\t" + rdr[1] + "\t" + rdr[2] + "  \t|  " + rdr[3] + "  \t|  " + rdr[4]);
                }
            }
            finally
            {
                // Close connection
                    rdr?.Close();
                    conn?.Close();
            }
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
                    (string)rdr["Fornavn"], 
                    (string)rdr["Mellemnavn"], 
                    (string)rdr["Efternavn"], 
                    (string)rdr["Type"],
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
        
        public List<AdresseModel> GetAdresses()
        {
            SqlDataReader sqlDataReader = null;
            List<AdresseModel> list = null;
            try
            {
              
                SqlCommand cmd = new SqlCommand("select * from Adresse", conn);
                conn.Open();
                sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    AdresseModel adresse = new AdresseModel(
                        Convert.ToInt32(sqlDataReader["AdresseId"]),
                        (int)sqlDataReader["husNummer"],
               
                        );
                    int id = (int)sqlDataReader["AdresseId"];
                    int HusNummer = (int) sqlDataReader["HusNummer"];
                    int PostNummer = (int) sqlDataReader["PostNummer"];
                    string Vejnavn = (string) sqlDataReader["Vejnavn"];
                    string Bynavn = (string) sqlDataReader["Bynavn"];
                    string Type = (string) sqlDataReader["Type"];

                    list.Add(new AdresseModel(id,Vejnavn,Bynavn,HusNummer,PostNummer,Type));
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
    }
}

