using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
                if (conn != null)
                    conn.Close();
            }
            return count;
        }

        public List<PersonModel> GetPersons()
        {
            SqlDataReader rdr = null;
            List<PersonModel> list = new List<PersonModel>();

            try
            {
                // Open connection
                conn.Open();

                // Instantiate a new command
                SqlCommand cmd = new SqlCommand("select * from Person", conn);

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
                        Convert.ToInt32(rdr["AdresseId"]));

                    list.Add(person);
                }
            }
            finally
            {
                // Close connection
                if (rdr != null)
                    rdr.Close();


                if (conn != null)
                    conn.Close();
            }

            return list;
        }

        public List<TelefonModel> GetTelefons(int personId, bool sortAsc)
        {
            SqlDataReader rdr = null;
            List<TelefonModel> list = new List<TelefonModel>();
            try
            {
                // Open connection
                conn.Open();

                string cmdString = "select * from TelefonNr where (PersonId = #personId) ORDER BY Nummer #sort";

                cmdString = cmdString.Replace("#personId", personId.ToString());
                cmdString = cmdString.Replace("#sort", (sortAsc ? "ASC" : "DSC"));


                // Instantiate a new command
                SqlCommand cmd = new SqlCommand(cmdString, conn);

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
                if (rdr != null)
                    rdr.Close();


                if (conn != null)
                    conn.Close();
            }

            return list;
        }

        public TelefonModel GetHomeTelefon(int personId)
        {
            SqlDataReader rdr = null;
            TelefonModel phone = null;
            try
            {
                // Open connection
                conn.Open();

                string cmdString = "select * from TelefonNr where (PersonId = #personId) AND (Type = 'Hjem')";

                // Instantiate a new command
                SqlCommand cmd = new SqlCommand(cmdString.Replace("#personId", personId.ToString()), conn);

                // Send command
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    phone = new TelefonModel(
                    Convert.ToInt32(rdr["TelefonId"]),
                    rdr["Type"].ToString(),
                    rdr["Nummer"].ToString(),
                   Convert.ToInt32(rdr["PersonId"]));

                    break;
                }
            }
            finally
            {
                // Close connection
                if (rdr != null)
                    rdr.Close();


                if (conn != null)
                    conn.Close();
            }

            return phone;
        }
    }
}

