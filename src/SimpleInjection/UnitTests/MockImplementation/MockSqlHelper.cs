
using System.Data;
using Core.Contracts;

namespace UnitTests.MockImplementation
{
    public class MockSqlHelperFactory : ISqlHelperFactory
    {
        public ISqlHelper GetSqlHelper()
        {
            return new MockSqlHelper("");
        }
        public class MockSqlHelper : ISqlHelper
        {
            private readonly string _connectionString;

            public MockSqlHelper(string connectionString)
            {
                _connectionString = connectionString;
            }

            public void Dispose()
            {

            }

            public void ExecuteNonQuery(string sqlQuery)
            {

            }

            public void AddParam<T>(string key, T value)
            {

            }

            public DataTable GetTable(string sSQL)
            {
                using (DataTable table = new DataTable())
                {
                    // Two columns.
                    table.Columns.Add("FirstName", typeof(string));
                    table.Columns.Add("LastName", typeof(string));

                    table.Rows.Add("Foo", "Bar");
                    return table;
                }
            }
        }
    }
}