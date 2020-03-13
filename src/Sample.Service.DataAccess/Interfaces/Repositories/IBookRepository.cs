using Sample.Service.DataAccess.Dto;
using Sample.Service.Models.Models;

namespace Sample.Service.DataAccess.Interfaces.Repositories
{
    /// <summary>
    /// Book repository interface.
    /// </summary>
    public interface IBookRepository : IRepository<Book, BookDto>
    {
    }
}
