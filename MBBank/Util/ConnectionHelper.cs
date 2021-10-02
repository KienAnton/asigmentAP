using System.Data;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace MBBank.Util
{
    public class ConnectionHelper
    {
        private static MySqlConnection _connection;
        private static readonly string Server = "127.0.0.1";
        private static readonly string Username = "root";
        private static readonly string Password = "";
        private static readonly string Database = "t2012e_mbbank";  
        private static readonly string _connectionString = "server={0};uid={1};pwd={2};database={3};SslMode=none";
        
        //class method
        public static MySqlConnection GetInstance()
        {
            if (_connection == null || _connection.State == ConnectionState.Closed)
            {
                //string format
                _connection = new MySqlConnection(string.Format(_connectionString, Server, Username, Password, Database));
            }
            
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            return _connection;
        }
    }
}