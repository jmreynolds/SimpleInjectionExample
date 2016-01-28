using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Implementations;

namespace ConsoleHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            // First pass
            //FirstPass();

            // Second Pass
            SecondPass();
        }

        private static void SecondPass()
        {
            
        }

        static void FirstPass()
        {
            var firstPass = new PersonServiceFirstPass();
            var person = new Person();
            person.FirstName = "Joe";
            person.LastName = "Reynolds";
            var newId = firstPass.WritePerson(person);

            var readPerson = firstPass.GetPerson(newId);
            Console.WriteLine($"New Person ID: {readPerson.PersonId} {Environment.NewLine} " +
                              $"New Name: {readPerson.FirstName} {readPerson.LastName} ");
            Console.WriteLine("Did it work?");
            Console.ReadKey();
            firstPass.CleanUp();
        }
    }
}
