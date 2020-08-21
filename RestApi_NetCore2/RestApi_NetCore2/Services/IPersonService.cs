using RestApi_NetCore2.Data.VO;
using System.Collections.Generic;

namespace RestApi_NetCore2.Services.Implementations
{
    public interface IPersonService
    {
        PersonVO Create (PersonVO person);
        PersonVO FindById (long Id);
        List<PersonVO> FindAll ();
        PersonVO Update (PersonVO person);
        void Delete (long Id);
    }
}
