using Core.Contracts;

namespace DataAccess
{
    public class SqlHelperFactory : ISqlHelperFactory
    {
        private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;" +
                                                    "Initial Catalog=SimpleInjectionExample;" +
                                                    "Integrated Security=True;Connect Timeout=30;" +
                                                    "Encrypt=False;TrustServerCertificate=False;" +
                                                    "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ISqlHelper GetSqlHelper()
        {
            return new SqlHelper(_connectionString);
        }
    }
}