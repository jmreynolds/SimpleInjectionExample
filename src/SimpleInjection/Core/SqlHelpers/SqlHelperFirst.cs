using System;
using System.Data;
using System.Data.SqlClient;

namespace Core.SqlHelpers
{
    public class SqlHelperFirst : IDisposable
    {
        private readonly SqlCommand _sqlCommand;
        private readonly SqlConnection _sqlConnection;

        public SqlHelperFirst(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
            _sqlCommand = new SqlCommand { Connection = _sqlConnection };
            _sqlConnection.Open();
        }

        public void Close()
        {
            // close the connection
            if (_sqlConnection.State == ConnectionState.Open)
                _sqlConnection.Close();
        }

        public SqlCommand SqlCommand
        {
            get { return _sqlCommand; }
        }

        public DataSet GetDataSet()
        {
            var myAdpater = new SqlDataAdapter { SelectCommand = _sqlCommand };
            var myDataSet = new DataSet();

            myAdpater.Fill(myDataSet);
            return myDataSet;
        }

        public DataTable GetTable()
        {
            var ds = GetDataSet();
            var dt = ds.Tables[0];
            return dt;
        }

        public void AddParam<T>(string key, T value)
        {
            if (key.StartsWith("@")) key = key.Substring(1);
            _sqlCommand.Parameters.AddWithValue($"@{key}", value);
        }

        public void Dispose()
        {
            Close();
        }

        ~SqlHelperFirst()
        {
            if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                _sqlConnection.Close();
        }

    }
}