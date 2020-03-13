using Sample.Service.DataAccess.Dto;
using Sample.Service.Models.Models;

namespace Sample.Service.Business.Interfaces
{
    /// <summary>
    /// Book business interface.
    /// </summary>
    public interface IBookBusiness : IBusiness<Book, BookDto>
    {
    }
}
