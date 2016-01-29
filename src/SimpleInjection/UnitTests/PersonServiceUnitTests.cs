using System;
using System.Data;
using System.Data.SqlClient;
using Core;
using Core.Contracts;
using Core.Fakes;
using Core.Implementations;
using Core.SqlHelpers;
using NUnit.Framework;
using Should;

namespace UnitTests
{
    public class PersonServiceUnitTests
    {
        [Test]
        public void ShouldUseFake()
        {
            var factory = new FakeSqlHelperFactory();
            var personService = new PersonServiceFourthPass(factory);
            var expectedFirst = "Foo";
            var expectedLast = "Bar";

            var person = personService.GetPerson(Guid.NewGuid());

            expectedFirst.ShouldEqual(person.FirstName);
            expectedLast.ShouldEqual(person.LastName);
        }
    }

    public class PersonServiceIntegrationTests
    {
        string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;" +
                           "Initial Catalog=SimpleInjectionExample;" +
                           "Integrated Security=True;Connect Timeout=30;" +
                           "Encrypt=False;TrustServerCertificate=False;" +
                           "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private ISqlHelperFactory factory;
        private PersonServiceFourthPass personService;
        private Person person;

        [Test]
        public void ShouldWritePerson()
        {
            factory = new SqlHelperThirdFactory();
            personService = new PersonServiceFourthPass(factory);
            person = new Person {FirstName = "First", LastName = "Last"};
            var newId = personService.WritePerson(person);

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

            person.FirstName.ShouldEqual(result.FirstName);
            person.LastName.ShouldEqual(result.LastName);
            newId.ShouldEqual(result.PersonId);
        }

        [Test]
        public void ShouldReadPerson()
        {
            factory = new SqlHelperThirdFactory();
            personService = new PersonServiceFourthPass(factory);
            person = new Person { FirstName = "First", LastName = "Last" };
            var newId = personService.WritePerson(person);
            var readPerson = personService.GetPerson(newId);
            readPerson.FirstName.ShouldEqual(person.FirstName);
            readPerson.LastName.ShouldEqual(person.LastName);
            readPerson.PersonId.ShouldEqual(newId);
        }

        [TearDown]
        public void Cleanup()
        {
            factory = new SqlHelperThirdFactory();
            personService = new PersonServiceFourthPass(factory);
            personService.CleanUp();
        }
    }
}