using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Fakes;
using Core.Implementations;
using Core.SqlHelpers;

namespace ConsoleHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            // First pass
            //FirstPass();

            // Second Pass
            // SecondPass();

            // Third Pass
            // ThirdPass();

            // Fourth Pass
            // FourthPass();

            // Fake Pass
            FakePass();
        }



        static void FirstPass()
        {
            var personService = new PersonServiceFirstPass();
            var person = new Person();
            person.FirstName = "Joe";
            person.LastName = "Reynolds";
            var newId = personService.WritePerson(person);

            var readPerson = personService.GetPerson(newId);
            Console.WriteLine($"New Person ID: {readPerson.PersonId} {Environment.NewLine} " +
                              $"New Name: {readPerson.FirstName} {readPerson.LastName} ");
            Console.WriteLine("Did it work?");
            Console.ReadKey();
            personService.CleanUp();
        }

        private static void SecondPass()
        {
            var personService = new PersonServiceSecondPass();
            var person = new Person();
            person.FirstName = "Mark";
            person.LastName = "Reynolds";
            var newId = personService.WritePerson(person);

            var readPerson = personService.GetPerson(newId);
            Console.WriteLine($"New Person ID: {readPerson.PersonId} {Environment.NewLine} " +
                              $"New Name: {readPerson.FirstName} {readPerson.LastName} ");
            Console.WriteLine("Did it work?");
            Console.ReadKey();
            personService.CleanUp();
        }
        private static void ThirdPass()
        {
            var personService = new PersonServiceThirdPass();
            var person = new Person();
            person.FirstName = "Shannon";
            person.LastName = "Reynolds";
            var newId = personService.WritePerson(person);

            var readPerson = personService.GetPerson(newId);
            Console.WriteLine($"New Person ID: {readPerson.PersonId} {Environment.NewLine} " +
                              $"New Name: {readPerson.FirstName} {readPerson.LastName} ");
            Console.WriteLine("Did it work?");
            Console.ReadKey();
            personService.CleanUp();
        }

        private static void FourthPass()
        {
            var factory = new SqlHelperThirdFactory();
            var personService = new PersonServiceFourthPass(factory);
            var person = new Person();
            person.FirstName = "Judy";
            person.LastName = "Reynolds";
            var newId = personService.WritePerson(person);

            var readPerson = personService.GetPerson(newId);
            Console.WriteLine($"New Person ID: {readPerson.PersonId} {Environment.NewLine} " +
                              $"New Name: {readPerson.FirstName} {readPerson.LastName} ");
            Console.WriteLine("Did it work?");
            Console.ReadKey();
            personService.CleanUp();
        }

        private static void FakePass()
        {
            var factory = new FakeSqlHelperFactory();
            var personService = new PersonServiceFourthPass(factory);
            var person = new Person();
            person.FirstName = "Judy";
            person.LastName = "Reynolds";
            var newId = personService.WritePerson(person);

            var readPerson = personService.GetPerson(newId);
            Console.WriteLine($"New Person ID: {readPerson.PersonId} {Environment.NewLine} " +
                              $"New Name: {readPerson.FirstName} {readPerson.LastName} ");
            Console.WriteLine("Did it work?");
            Console.ReadKey();
            personService.CleanUp();
        }

    }
}
