using System;
using Core.Contracts;

namespace Core.Implementations
{
    public class PersonService
    {
        readonly ISqlHelperFactory _sqlHelperFactory;

        public PersonService(ISqlHelperFactory factory)
        {
            _sqlHelperFactory = factory;
        }

        public Guid WritePerson(Person person)
        {
            if (person.PersonId != Guid.Empty)
            {
                throw new ArgumentException("Person Id is not empty.");
            }
            string sSQL = "INSERT INTO Person " +
                          "(PersonId, FirstName, LastName, BirthDate)" +
                          "VALUES" +
                          "(@personId, @FirstName, @LastName, @BirthDate);";
            Guid identity = Guid.NewGuid();
            using (var sqlHelper = _sqlHelperFactory.GetSqlHelper())
            {
                sqlHelper.AddParam("personId", identity);
                sqlHelper.AddParam("FirstName", person.FirstName);
                sqlHelper.AddParam("LastName", person.LastName);
                sqlHelper.AddParam("BirthDate", person.BirthDate);
                sqlHelper.ExecuteNonQuery(sSQL);
            }
            return identity;
        }

        public Person GetPerson(Guid personId)
        {
            string sSQL = "SELECT FirstName, LastName, BirthDate " +
                            "FROM Person " +
                            "WHERE PersonId = @personId";
            Person result;
            using (var sqlHelper = _sqlHelperFactory.GetSqlHelper())
            {
                sqlHelper.AddParam("personId", personId);
                var table = sqlHelper.GetTable(sSQL);
                var firstName = table.Rows[0][0].ToString();
                var lastName = table.Rows[0][1].ToString();
                var birthDate = DateTime.Parse(table.Rows[0][2].ToString());
                result = new Person(personId, firstName, lastName, birthDate);
            }
            return result;
        }

        public void CleanUp()
        {
            string sSQL = "DELETE FROM Person";
            using (var sqlHelper = _sqlHelperFactory.GetSqlHelper())
            {
                sqlHelper.ExecuteNonQuery(sSQL);
            }
        }
    }
}
