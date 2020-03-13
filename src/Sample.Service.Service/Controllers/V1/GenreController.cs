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
    [Route("v{version:apiVersion}/genres")]
    [ApiController]
    [ServiceFilter(typeof(ValidateModelAttribute))]
    public class GenreController : ControllerBase
    {
        #region :: Properties ::

        /// <summary>
        /// Business.
        /// </summary>
        private readonly IGenreBusiness business;

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.Service.Controllers.V1.GenreController"/> class.
        /// </summary>
        /// <param name="business">Business.</param>
        public GenreController(IGenreBusiness business)
        {
            this.business = business;
        }

        #endregion

        #region :: Methods ::

        /// <summary>
        /// Add a genre.
        /// </summary>
        /// <param name="genreDto">Genre to add.</param>
        /// <returns>Genre added.</returns>
        [HttpPost]
        public async Task<IActionResult> AddGenre(GenreDto genreDto)
        {
            var result = await business.AddAsync(genreDto);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        /// <summary>
        /// Update a genre.
        /// </summary>
        /// <param name="genreDto">Genre to update.</param>
        /// <param name="id">Identifier.</param>
        /// <returns>The async.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(Guid id, GenreDto genreDto)
        {
            await business.UpdateAsync(genreDto, id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete a genre.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>The async.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(Guid id)
        {
            await business.DeleteAsync(id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Find all genres.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <returns>Genres found.</returns>
        [HttpGet]
        public async Task<IActionResult> FindAllGenre([FromQuery]FilterModelBase filter)
        {
            var result = await business.FindAllAsync(filter);
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Find a genres.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>Genre found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> FindAllGenre(Guid id)
        {
            var result = await business.FindByIdAsync(id);
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        #endregion
    }
}
