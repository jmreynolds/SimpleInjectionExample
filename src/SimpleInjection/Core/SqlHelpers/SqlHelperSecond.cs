using System;
using System.Data;
using System.Data.SqlClient;

namespace Core.SqlHelpers
{
    public class SqlHelperSecond : IDisposable
    {
        private readonly SqlCommand _sqlCommand;
        private readonly SqlConnection _sqlConnection;

        public SqlHelperSecond(string connectionString)
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

        public void ClearParameters()
        {
            _sqlCommand.Parameters.Clear();
        }

        public DataTable GetTable(string sSQL)
        {
            _sqlCommand.CommandText = sSQL;
            var myAdpater = new SqlDataAdapter { SelectCommand = _sqlCommand };
            DataTable dt=new DataTable();
            myAdpater.Fill(dt);
            return dt;
        }

        public void Dispose()
        {
            if (_sqlConnection.State == ConnectionState.Open)
                _sqlConnection.Close();
        }
    }
}