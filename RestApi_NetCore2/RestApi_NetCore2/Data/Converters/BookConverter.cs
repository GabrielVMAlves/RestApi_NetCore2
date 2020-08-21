using RestApi_NetCore2.Data.Converter;
using RestApi_NetCore2.Data.VO;
using RestApi_NetCore2.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestApi_NetCore2.Data.Converters
{
    public class BookConverter : IParser<Book, BookVO>, IParser<BookVO, Book>
    {
        public BookVO Parse(Book origin)
        {
            if (origin == null) return null;
            return new BookVO
            {
                Id = origin.Id,
                Author = origin.Author,
                Title = origin.Title,
                Price = origin.Price,
                LaunchDate = origin.LaunchDate
            };
        }

        public List<BookVO> Parse(List<Book> origins)
        {
            if (origins.Count == 0 || origins == null) return null;
            return origins.Select(x => Parse(x)).ToList();
        }

        public Book Parse(BookVO origin)
        {
            if (origin == null) return null;
            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                Title = origin.Title,
                Price = origin.Price,
                LaunchDate = origin.LaunchDate
            };
        }

        public List<Book> Parse(List<BookVO> origins)
        {
            if (origins.Count == 0 || origins == null) return null;
            return origins.Select(x => Parse(x)).ToList();
        }
    }
}
