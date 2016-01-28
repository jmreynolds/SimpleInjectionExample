﻿using System;
using System.Data.SqlClient;
using Core.SqlHelpers;

namespace Core.Implementations
{
    public class PersonServiceThirdPass
    {
        string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;" +
                          "Initial Catalog=SimpleInjectionExample;" +
                          "Integrated Security=True;Connect Timeout=30;" +
                          "Encrypt=False;TrustServerCertificate=False;" +
                          "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private SqlDataReader _myReader;

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
            using (var sqlHelper = new SqlHelperSecond(_connectionString))
            {
                sqlHelper.AddParam("personId", identity);
                sqlHelper.AddParam("FirstName", person.FirstName);
                sqlHelper.AddParam("LastName", person.LastName);
                sqlHelper.ExecuteNonQuery(sSQL);
            }
            return identity;
        }

        public Person GetPerson(Guid personId)
        {
            string sSQL = "SELECT FirstName, LastName " +
                          "FROM Person " +
                          "WHERE PersonId = @personId";
            Person result;
            using (var sqlHelper = new SqlHelperSecond(_connectionString))
            {
                sqlHelper.AddParam("personId", personId);
                var table = sqlHelper.GetTable(sSQL);
                var firstName = table.Rows[0][0].ToString();
                var lastName = table.Rows[0][1].ToString();
                result = new Person { PersonId = personId, FirstName = firstName, LastName = lastName };
            }
            return result;
        }

        public void CleanUp()
        {
            string sSQL = "DELETE FROM Person";
            using (var sqlHelper = new SqlHelperSecond(_connectionString))
            {
                sqlHelper.ExecuteNonQuery(sSQL);
            }
        }

    }
}