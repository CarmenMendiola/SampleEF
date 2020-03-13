using AutoMapper;
using Sample.Service.DataAccess.Dto;
using Sample.Service.Models.Models;

namespace Sample.Service.DataAccess.Extensions
{
    /// <summary>
    /// Mapping profile.
    /// </summary>
    public class MappingProfile : Profile
    {
        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Checkout.Service.DataAccess.Extensions.MappingProfile"/> class.
        /// Mapping the Dto's against their model entity.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Genre, GenreDto>().ReverseMap();
        }

        #endregion
    }
}
