using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Implementations
{
    public class PersonServiceFirstPass
    {
        string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;" +
                                   "Initial Catalog=SimpleInjectionExample;" +
                                   "Integrated Security=True;Connect Timeout=30;" +
                                   "Encrypt=False;TrustServerCertificate=False;" +
                                   "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public Guid WritePerson(Person person)
        {
            if (person.PersonId != Guid.Empty)
            {
                throw new ArgumentException("Person Id is not empty.");
            }
            string sSQL = "INSERT INTO Person " +
                          "(PersonId, FirstName, LastName)" +
                          "VALUES" +
                          "(@personId, @FirstName, @LastName);" +
                          "SELECT SCOPE_IDENTITY()";
            Guid identity = Guid.NewGuid();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
               connection.Open();
                SqlCommand command = new SqlCommand(sSQL, connection);
                command.Parameters.Add("@personId", SqlDbType.UniqueIdentifier).Value = identity;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = person.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = person.LastName;
                command.ExecuteNonQuery();
            }
            return identity;
        }

        public Person GetPerson(Guid personId)
        {
            string sSQL = "SELECT FirstName, LastName " +
                          "FROM Person " +
                          "WHERE PersonId = @PersonId";
            Person result;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sSQL, connection);
                command.Parameters.Add("@PersonId", SqlDbType.UniqueIdentifier).Value = personId;
                var reader = command.ExecuteReader();
                var table = new DataTable();
                table.Load(reader);
                var firstName = table.Rows[0][0].ToString();
                var lastName = table.Rows[0][1].ToString();
                result = new Person {PersonId = personId, FirstName = firstName, LastName = lastName};
            }
            return result;
        }

        public void CleanUp()
        {
            string sSQL = "DELETE FROM Person";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sSQL, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
