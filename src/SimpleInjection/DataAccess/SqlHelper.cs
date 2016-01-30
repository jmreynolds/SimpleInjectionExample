using System.Data;
using System.Data.SqlClient;
using Core.Contracts;

namespace DataAccess
{
    public class SqlHelper : ISqlHelper
    {
        private readonly SqlCommand _sqlCommand;
        private readonly SqlConnection _sqlConnection;

        public SqlHelper(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
            _sqlCommand = new SqlCommand { Connection = _sqlConnection };
            _sqlConnection.Open();
            ClearParameters();
        }

        public void ExecuteNonQuery(string sqlQuery)
        {
            _sqlCommand.CommandText = sqlQuery;
            _sqlCommand.ExecuteNonQuery();
        }

        public void AddParam<T>(string key, T value)
        {
            if (key.StartsWith("@")) key = key.Substring(1);
            _sqlCommand.Parameters.AddWithValue($"@{key}", value);
        }

        public DataTable GetTable(string sSQL)
        {
            _sqlCommand.CommandText = sSQL;
            var myAdpater = new SqlDataAdapter { SelectCommand = _sqlCommand };
            var dt = new DataTable();
            myAdpater.Fill(dt);
            return dt;
        }

        public void Dispose()
        {
            if (_sqlConnection.State == ConnectionState.Open)
                _sqlConnection.Close();
        }

        public void ClearParameters()
        {
            _sqlCommand.Parameters.Clear();
        }
    }
}