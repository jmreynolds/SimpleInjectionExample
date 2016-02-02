using System.Data;
using System.Data.SqlClient;
using Core.Contracts;

namespace DataAccess
{
    public class SqlHelper : ISqlHelper
    {
        public SqlCommand Command { get; }
        public SqlConnection Connection { get; }

        public SqlHelper(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Command = new SqlCommand {Connection = Connection};
            Connection.Open();
            ClearParameters();
        }


        public void ExecuteNonQuery(string sqlQuery)
        {
            Command.CommandText = sqlQuery;
            Command.ExecuteNonQuery();
        }

        public void AddParam<T>(string key, T value)
        {
            if (key.StartsWith("@")) key = key.Substring(1);
            Command.Parameters.AddWithValue($"@{key}", value);
        }

        public DataTable GetTable(string sSQL)
        {
            Command.CommandText = sSQL;
            var myAdpater = new SqlDataAdapter {SelectCommand = Command};
            var dt = new DataTable();
            myAdpater.Fill(dt);
            return dt;
        }

        public void Dispose()
        {
            if (Connection.State == ConnectionState.Open)
                Connection.Close();
        }

        public void ClearParameters() => Command.Parameters.Clear();
    }
}