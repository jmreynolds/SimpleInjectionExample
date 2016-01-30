using System;
using Core.Implementations;
using NUnit.Framework;
using Should;
using UnitTests.MockImplementation;

namespace UnitTests
{
    [TestFixture]
    public class InjectionTester
    {
        [Test]
        public void ShouldReturnFooBarName()
        {
            var factory = new MockSqlHelperFactory();
            var personService = new PersonService(factory);
            const string expectedFirst = "Foo";
            const string expectedLast = "Bar";

            var person = personService.GetPerson(Guid.NewGuid());

            expectedFirst.ShouldEqual(person.FirstName);
            expectedLast.ShouldEqual(person.LastName);
        }
    }
}
