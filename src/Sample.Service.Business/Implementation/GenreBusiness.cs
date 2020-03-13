using Sample.Service.Business.Interfaces;
using Sample.Service.DataAccess.Dto;
using Sample.Service.DataAccess.Interfaces.Repositories;
using Sample.Service.Models.Models;

namespace Sample.Service.Business.Implementation
{
    public class GenreBusiness : Business<Genre, GenreDto>, IGenreBusiness
    {
        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.Business.Implementation.GenreBusiness"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public GenreBusiness(IGenreRepository repository) : base(repository)
        {
        }

        #endregion
    }
}
