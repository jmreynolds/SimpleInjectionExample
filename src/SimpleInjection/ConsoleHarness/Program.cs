using System;
using Core;
using Core.Implementations;
using static System.Console;

namespace ConsoleHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            RunService();
        }

        static void RunService()
        {
            var personService = new PersonService();
            var person = new Person
            {
                FirstName = "Joe",
                LastName = "Reynolds"
            };
            var newId = personService.WritePerson(person);

            var readPerson = personService.GetPerson(newId);
            WriteLine($"New Person ID: {readPerson.PersonId}");
            WriteLine($"New Name: {readPerson.FirstName} {readPerson.LastName}");
            WriteLine("Did it work?");
            ReadKey();
            personService.CleanUp();
        }

    }
}
