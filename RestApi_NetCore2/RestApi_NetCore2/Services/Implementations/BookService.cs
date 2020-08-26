using System.Collections.Generic;
using RestApi_NetCore2.Data.Converters;
using RestApi_NetCore2.Data.VO;
using RestApi_NetCore2.Models;
using RestApi_NetCore2.Repository.Generic;

namespace RestApi_NetCore2.Services.Implementations
{
    public class BookService : IBookService
    {
        private IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookService(IRepository<Book> bookRepository)
        {
            _repository = bookRepository;
            _converter = new BookConverter();

        }
        public BookVO Create(BookVO book)
        {
            Book bookEntity = _converter.Parse(book);
            return _converter.Parse(_repository.Create(bookEntity));
        }

        public void Delete(long Id)
        {
            _repository.Delete(Id);
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindById(long Id)
        {
            return _converter.Parse(_repository.FindById(Id));
        }

        public BookVO Update(BookVO book)
        {
            Book bookEntity = _converter.Parse(book);
            return _converter.Parse(_repository.Update(bookEntity));
        }
    }
}
