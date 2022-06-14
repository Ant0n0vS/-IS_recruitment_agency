using System.Data.SqlClient;


namespace Recruitment_agency
{
    class Database
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-1SF92FOB;
        Initial Catalog=Recruitment agency;Integrated Security=True");

        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection getConnection()
        {
            return sqlConnection;
        }
    }
}
