using AutoMapper;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.Entities;
using static WebApi.BookOperations.CreateBooks.CreateBookCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(x => x.Genre, opt => opt.MapFrom(y => ((GenreEnum)y.GenreId).ToString()));
            CreateMap<Book, BookViewModel>().ForMember(x => x.Genre, opt => opt.MapFrom(y => ((GenreEnum)y.GenreId).ToString()));
        }
    }
}