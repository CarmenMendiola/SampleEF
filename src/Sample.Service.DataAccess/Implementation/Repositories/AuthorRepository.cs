using AutoMapper;
using Sample.Service.DataAccess.Dto;
using Sample.Service.DataAccess.Dto.Common;
using Sample.Service.DataAccess.Interfaces.Repositories;
using Sample.Service.Models;
using Sample.Service.Models.Models;

namespace Sample.Service.DataAccess.Implementation.Repositories
{
    /// <summary>
    /// Author repository.
    /// </summary>
    public class AuthorRepository : Repository<Author, AuthorDto>, IAuthorRepository
    {
        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.DataAccess.Implementation.Repositories.AuthorRepository`1"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="mapper">Mapper.</param>
        /// <param name="messages">Custom messages.</param>
        public AuthorRepository(SampleDbContext context, IMapper mapper, CustomMessages messages) :
            base(context, mapper, messages)
        { }

        #endregion
    }
}
