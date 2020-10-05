using RestApi_NetCore2.Models;
using System.Collections.Generic;

namespace RestApi_NetCore2.Repository
{
    public interface IUserRepository
    {
        User Create(User person);
        User FindById(long Id);
        User FindByUsername(string username);
        List<User> FindAll();
        User Update(User person);
        void Delete(long Id);
    }
}
