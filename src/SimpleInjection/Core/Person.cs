using System;

namespace Core
{
public class Person
{

    public Person()
    {
    }

    public Person(Guid personId, string firstName, string lastName)
    {
        PersonId = personId;
        FirstName = firstName;
        LastName = lastName;
    }

    public Guid PersonId { get; } = Guid.Empty;

    public string FirstName { get; set; }
    public string LastName { get; set; }
}
}