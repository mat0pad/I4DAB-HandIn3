using System.Data.SqlClient;

namespace I4DAB_HandIn3
{
    public class DatabaseUtil
    {
        private SqlConnection conn;

        public void Setup()
        {
            // Instantiate the connection
            conn = new SqlConnection("Data Source=i4dab.ase.au.dk;Initial Catalog=F17I4DABH2Gr18;User ID=F17I4DABH2Gr18; Password=F17I4DABH2Gr18");
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

        public string GetPersons()
        {
            string count = "-1";

            try
            {
                // Open connection
                conn.Open();

                // Instantiate a new command
                SqlCommand cmd = new SqlCommand("select Fornavn from Person", conn);

                // Send command
                cmd.ExecuteScalar();
            }
            finally
            {
                // Close connection
                if (conn != null)
                    conn.Close();
            }
            return count;
        }
    }
}