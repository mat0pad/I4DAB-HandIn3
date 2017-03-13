using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace I4DAB_HandIn3
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
                    Console.WriteLine(rdr[0] + "\t" + rdr[1] + "\t" + rdr[2] + "  \t|  " + rdr[3] + "  \t\t|  " + rdr[4]);
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
        }
    }
}