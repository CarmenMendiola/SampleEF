using Sample.Service.DataAccess.Dto;
using Sample.Service.Models.Models;

namespace Sample.Service.DataAccess.Interfaces.Repositories
{
    /// <summary>
    /// Author repository interface.
    /// </summary>
    public interface IAuthorRepository:IRepository<Author, AuthorDto>
    {
    }
}
