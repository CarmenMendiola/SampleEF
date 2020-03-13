using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.Service.Business.Interfaces;
using Sample.Service.DataAccess.Dto;
using Sample.Service.Models.Filters;
using Sample.Service.Service.Filters;

namespace Sample.Service.Service.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [Route("v{version:apiVersion}/books")]
    [ApiController]
    [ServiceFilter(typeof(ValidateModelAttribute))]
    public class BookController : ControllerBase
    {
        #region :: Properties ::

        /// <summary>
        /// Business.
        /// </summary>
        private readonly IBookBusiness business;

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.Service.Controllers.V1.BookController"/> class.
        /// </summary>
        /// <param name="business">Business.</param>
        public BookController(IBookBusiness business)
        {
            this.business = business;
        }

        #endregion

        #region :: Methods ::

        /// <summary>
        /// Add a book.
        /// </summary>
        /// <param name="bookDto">Book to add.</param>
        /// <returns>Book added.</returns>
        [HttpPost]
        public async Task<IActionResult> AddBook(BookDto bookDto)
        {
            var result = await business.AddAsync(bookDto);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        /// <summary>
        /// Update a book.
        /// </summary>
        /// <param name="bookDto">Book to update.</param>
        /// <param name="id">Identifier.</param>
        /// <returns>The async.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, BookDto bookDto)
        {
            await business.UpdateAsync(bookDto, id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete a book.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>The async.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            await business.DeleteAsync(id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Find all books.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <returns>Books found.</returns>
        [HttpGet    ]
        public async Task<IActionResult> FindAllBook([FromQuery]FilterModelBase filter)
        {
            var result = await business.FindAllAsync(filter);
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Find a books.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>Book found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> FindAllBook(Guid id)
        {
            var result = await business.FindByIdAsync(id);
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        #endregion
    }
}
