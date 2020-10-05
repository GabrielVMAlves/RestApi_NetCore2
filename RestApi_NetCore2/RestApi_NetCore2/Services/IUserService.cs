using RestApi_NetCore2.Models;
using System.Collections.Generic;

namespace RestApi_NetCore2.Services.Implementations
{
    public interface IUserService
    {
        User Create(User user);
        User FindById(long Id);
        object FindByUsername(User user);
        List<User> FindAll();
        User Update(User user);
        void Delete(long Id);
    }
}
