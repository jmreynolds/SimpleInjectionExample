using System;

namespace Core
{
    public class Person
    {
        public Person()
        {
        }

        public Guid PersonId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}