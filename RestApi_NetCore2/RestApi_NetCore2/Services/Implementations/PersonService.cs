using System;
using System.Collections.Generic;
using RestApi_NetCore2.Models;

namespace RestApi_NetCore2.Services.Implementations
{
    public class PersonService : IPersonService
    {
        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long Id)
        {

        }

        public List<Person> FindAll(Person person)
        {
            return new List<Person>();
        }

        public Person FindById(long Id)
        {
            return new Person();
        }

        public Person Update(Person person)
        {
            return new Person();
        }
    }
}
