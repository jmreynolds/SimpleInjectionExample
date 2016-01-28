using System;
using System.Data;
using System.Data.SqlClient;

namespace Core
{
    public class SqlHelper : IDisposable
    {
        private readonly SqlCommand _sqlCommand;
        private readonly SqlConnection _sqlConnection;

        public SqlHelper(string connectionString)
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

        public SqlDataReader GetDataReader(string sqlCommand)
        {
            _sqlCommand.CommandText = sqlCommand;
            return _sqlCommand.ExecuteReader();
        }

        public SqlDataReader GetDataReader()
        {
            return _sqlCommand.ExecuteReader();
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
        public void SetupSproc(string getbenefitsummaryemployee)
        {
            _sqlCommand.CommandText = getbenefitsummaryemployee;
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.Parameters.Clear();
        }

        public void AddParam<T>(string key, T value)
        {
            if (key.StartsWith("@")) key = key.Substring(1);
            _sqlCommand.Parameters.AddWithValue($"@{key}", value);
        }
        public void AddOutputParam(string paramName, SqlDbType sqlDbType)
        {
            if (paramName.StartsWith("@")) paramName = paramName.Substring(1);
            // Create parameter with Direction as Output (and correct name and type)
            var outputIdParam = new SqlParameter($"@{paramName}", sqlDbType)
            {
                Direction = ParameterDirection.Output
            };
            _sqlCommand.Parameters.Add(outputIdParam);
        }


        public void Dispose()
        {
            Close();
        }

        ~SqlEasyReader()
        {
            if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                _sqlConnection.Close();
        }

    }
}