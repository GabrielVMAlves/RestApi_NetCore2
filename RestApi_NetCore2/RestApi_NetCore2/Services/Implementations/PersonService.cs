using System.Collections.Generic;
using RestApi_NetCore2.Data.Converters;
using RestApi_NetCore2.Data.VO;
using RestApi_NetCore2.Models;
using RestApi_NetCore2.Repository.Generic;

namespace RestApi_NetCore2.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonService(IRepository<Person> personRepository)
        {
            _repository = personRepository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            Person personEntity = _converter.Parse(person);
            return _converter.Parse(_repository.Create(personEntity));
        }

        public void Delete(long Id)
        {
            _repository.Delete(Id);
        }

        public List<PersonVO> FindAll()
        {

            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindById(long Id)
        {
            return _converter.Parse(_repository.FindById(Id));
        }

        public PersonVO Update(PersonVO person)
        {
            Person personEntity = _converter.Parse(person);
            return _converter.Parse(_repository.Update(personEntity));
        }
    }
}
