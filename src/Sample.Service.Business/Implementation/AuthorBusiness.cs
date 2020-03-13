using Sample.Service.Business.Interfaces;
using Sample.Service.DataAccess.Dto;
using Sample.Service.DataAccess.Interfaces.Repositories;
using Sample.Service.Models.Models;

namespace Sample.Service.Business.Implementation
{
    public class AuthorBusiness : Business<Author, AuthorDto>, IAuthorBusiness
    {
        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.Business.Implementation.AuthorBusiness"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public AuthorBusiness(IAuthorRepository repository) : base(repository)
        {
        }

        #endregion
    }
}
