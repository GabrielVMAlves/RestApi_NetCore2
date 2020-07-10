using System;
using System.Collections.Generic;
using RestApi_NetCore2.Models;
using RestApi_NetCore2.Repository;

namespace RestApi_NetCore2.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _repository;

        public PersonService(IPersonRepository personRepository)
        {
            _repository = personRepository;
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public void Delete(long Id)
        {
            _repository.Delete(Id);
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        public Person FindById(long Id)
        {
            return _repository.FindById(Id);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }
    }
}
