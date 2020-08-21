using RestApi_NetCore2.Models.Base;
using System.Collections.Generic;

namespace RestApi_NetCore2.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long Id);
        List<T> FindAll();
        T Update(T person);
        void Delete(long Id);
        bool Exists(long? Id);
    }
}
