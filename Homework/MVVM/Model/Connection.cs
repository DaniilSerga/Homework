using System.Data.SqlClient;
using System.IO;

namespace Homework.MVVM.Model
{
    public static class Connection
    {
        // Gets SqlConnection using connection string (works for every device)
        public static SqlConnection GetConnection()
        {
            string dbPath = Path.GetFullPath("..\\..\\..\\DataBase\\PlaceboDB.mdf");

            return new SqlConnection($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True");
        }
    }
}
