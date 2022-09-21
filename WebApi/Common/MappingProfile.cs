using AutoMapper;
using WebApi.BookOperations.CreateBooks;
using WebApi.Entities;
using static WebApi.BookOperations.CreateBooks.CreateBookCommand;

namespace WebApi.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
        }
    }
}
