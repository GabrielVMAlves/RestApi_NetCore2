using RestApi_NetCore2.Models;
using System.Collections.Generic;

namespace RestApi_NetCore2.Repository
{
    public interface IPersonRepository
    {
        Person Create(Person person);
        Person FindById(long Id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long Id);
    }
}
