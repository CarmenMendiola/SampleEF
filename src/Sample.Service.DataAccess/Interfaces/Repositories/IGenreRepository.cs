using Sample.Service.DataAccess.Dto;
using Sample.Service.Models.Models;

namespace Sample.Service.DataAccess.Interfaces.Repositories
{
    /// <summary>
    /// Genre repository interface.
    /// </summary>
    public interface IGenreRepository:IRepository<Genre, GenreDto>
    {
    }
}
