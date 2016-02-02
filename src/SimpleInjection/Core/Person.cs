using System;

namespace Core
{
    public class Person
    {
        public Person()
        {
        }

        public Person(Guid personId, string firstName, string lastName, DateTime birthDate)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public Guid PersonId { get; } = Guid.Empty;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public int Age => BirthDate == default(DateTime) ? 0 :  DateTime.Now.Year - BirthDate.Year;
    }
}