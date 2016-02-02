using System;
using Core;
using Core.Implementations;
using DataAccess;
using static System.Console;

namespace ConsoleHarness
{
    class Program
    {
        static void Main(string[] args) => RunService();

        static void RunService()
        {
            var personService = new PersonService(new SqlHelperFactory());
            var person = new Person
            {
                FirstName = "Joe",
                LastName = "Reynolds",
                BirthDate = DateTime.Parse("1/1/1900")
            };
            var newId = personService.WritePerson(person);
            WriteLine(newId);

            var readPerson = personService.GetPerson(newId);
            WriteLine($"New Person ID: {readPerson.PersonId}");
            WriteLine($"New Name: {readPerson.FirstName} {readPerson.LastName}");
            WriteLine($"Birthday: {readPerson.BirthDate.ToShortDateString()}");
            WriteLine("Did it work?");
            ReadKey();
            personService.CleanUp();
        }

    }
}
