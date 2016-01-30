using System.Data;
using System.Data.SqlClient;
using Core;
using Core.Contracts;
using Core.Implementations;
using DataAccess;
using NUnit.Framework;
using Should;

namespace IntegrationTests
{
    public class InjectionTester
    {
        string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;" +
                           "Initial Catalog=SimpleInjectionExample;" +
                           "Integrated Security=True;Connect Timeout=30;" +
                           "Encrypt=False;TrustServerCertificate=False;" +
                           "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private readonly ISqlHelperFactory _factory = new SqlHelperFactory();
        private PersonService _personService;
        private Person _person;

        [Test]
        public void ShouldWritePerson()
        {
            _personService = new PersonService(_factory);
            _person = new Person { FirstName = "First", LastName = "Last" };
            var newId = _personService.WritePerson(_person);

            // Yucky Sql Stuff to verify
            string sSQL = "SELECT FirstName, LastName " +
              "FROM Person " +
              "WHERE PersonId = @PersonId";
            Person result;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sSQL, connection);
                command.Parameters.Add("@PersonId", SqlDbType.UniqueIdentifier).Value = newId;
                var reader = command.ExecuteReader();
                var table = new DataTable();
                table.Load(reader);
                var firstName = table.Rows[0][0].ToString();
                var lastName = table.Rows[0][1].ToString();
                result = new Person { PersonId = newId, FirstName = firstName, LastName = lastName };
            }

            _person.FirstName.ShouldEqual(result.FirstName);
            _person.LastName.ShouldEqual(result.LastName);
            newId.ShouldEqual(result.PersonId);
        }

        [Test]
        public void ShouldReadPerson()
        {
            _personService = new PersonService(_factory);
            _person = new Person { FirstName = "First", LastName = "Last" };
            var newId = _personService.WritePerson(_person);
            var readPerson = _personService.GetPerson(newId);
            readPerson.FirstName.ShouldEqual(_person.FirstName);
            readPerson.LastName.ShouldEqual(_person.LastName);
            readPerson.PersonId.ShouldEqual(newId);
        }

        [TearDown]
        public void Cleanup()
        {
            _personService = new PersonService(_factory);
            _personService.CleanUp();
        }
    }
}
