using Sample.Service.DataAccess.Dto;
using Sample.Service.Models.Models;

namespace Sample.Service.Business.Interfaces
{
    /// <summary>
    /// Genre business interface.
    /// </summary>
    public interface IGenreBusiness : IBusiness<Genre, GenreDto>
    {
    }
}
