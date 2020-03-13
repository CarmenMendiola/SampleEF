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
    [Route("v{version:apiVersion}/authors")]
    [ApiController]
    [ServiceFilter(typeof(ValidateModelAttribute))]
    public class AuthorController : ControllerBase
    {
        #region :: Properties ::

        /// <summary>
        /// Business.
        /// </summary>
        private readonly IAuthorBusiness business;

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.Service.Controllers.V1.AuthorController"/> class.
        /// </summary>
        /// <param name="business">Business.</param>
        public AuthorController(IAuthorBusiness business)
        {
            this.business = business;
        }

        #endregion

        #region :: Methods ::

        /// <summary>
        /// Add a author.
        /// </summary>
        /// <param name="authorDto">Author to add.</param>
        /// <returns>Author added.</returns>
        [HttpPost]
        public async Task<IActionResult> AddAuthor(AuthorDto authorDto)
        {
            var result = await business.AddAsync(authorDto);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        /// <summary>
        /// Update a author.
        /// </summary>
        /// <param name="authorDto">Author to update.</param>
        /// <param name="id">Identifier.</param>
        /// <returns>The async.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(Guid id, AuthorDto authorDto)
        {
            await business.UpdateAsync(authorDto, id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete a author.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>The async.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            await business.DeleteAsync(id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Find all authors.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <returns>Authors found.</returns>
        [HttpGet]
        public async Task<IActionResult> FindAllAuthor([FromQuery]FilterModelBase filter)
        {
            var result = await business.FindAllAsync(filter);
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Find a authors.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>Author found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> FindAllAuthor(Guid id)
        {
            var result = await business.FindByIdAsync(id);
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        #endregion
    }
}
