using System;

namespace Core
{
    public class Person
    {
        public Person()
        {
            PersonId = Guid.Empty;
        }

        public Guid PersonId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}