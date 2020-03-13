using Sample.Service.Business.Interfaces;
using Sample.Service.DataAccess.Dto;
using Sample.Service.DataAccess.Interfaces.Repositories;
using Sample.Service.Models.Models;

namespace Sample.Service.Business.Implementation
{
    public class BookBusiness : Business<Book, BookDto>, IBookBusiness
    {
        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.Business.Implementation.BookBusiness"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public BookBusiness(IBookRepository repository) : base(repository)
        {
        }

        #endregion
    }
}
