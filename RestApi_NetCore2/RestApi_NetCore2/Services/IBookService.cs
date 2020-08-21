using RestApi_NetCore2.Data.VO;
using System.Collections.Generic;

namespace RestApi_NetCore2.Services
{
    public interface IBookService
    {
        BookVO Create(BookVO book);
        BookVO FindById(long Id);
        List<BookVO> FindAll();
        BookVO Update(BookVO book);
        void Delete(long Id);
    }
}
