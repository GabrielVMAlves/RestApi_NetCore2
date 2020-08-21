using RestApi_NetCore2.Data.Converter;
using RestApi_NetCore2.Models;
using RestApi_NetCore2.Data.VO;
using System.Collections.Generic;
using System.Linq;

namespace RestApi_NetCore2.Data.Converters
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parse(PersonVO origin)
        {
            if (origin == null) return null;
            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Gender = origin.Gender,
                Address = origin.Address
            };
        }

        public List<Person> Parse(List<PersonVO> origins)
        {
            if (origins.Count == 0 || origins == null) return null;
            return origins.Select(x => Parse(x)).ToList();
        }

        public PersonVO Parse(Person origin)
        {
            if (origin == null) return null;
            return new PersonVO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Gender = origin.Gender,
                Address = origin.Address
            };
        }

        public List<PersonVO> Parse(List<Person> origins)
        {
            if (origins.Count == 0 || origins == null) return null;
            return origins.Select(x => Parse(x)).ToList();
        }
    }
}
