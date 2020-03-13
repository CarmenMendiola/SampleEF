using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Sample.Service.DataAccess.Interfaces;

namespace Sample.Service.DataAccess.Dto
{
    /// <summary>
    /// Book DTO.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class BookDto : IDto
    {
        #region :: Properties ::

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid? Id { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        [StringLength(250), RegularExpression(@"[0-9a-zA-Z\s\.\-\,ÑñÁÉÍÓÚáéíóúüÜ]+")]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the genre identifier.
        /// </summary>
        /// <value>The genre identifier.</value>
        public Guid GenreId { get; set; }

        /// <summary>
        /// Gets the genre.
        /// </summary>
        /// <value>The genre.</value>
        public GenreDto? Genre { get; private set; }

        /// <summary>
        /// Gets the author books.
        /// </summary>
        public List<AuthorDto>? Authors { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        public DateTime? CreatedAt { get; set; }

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.DataAccess.Dto.BookDto"/> class.
        /// </summary>
        public BookDto()
        {
            Name = string.Empty;
            GenreId = Guid.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.DataAccess.Dto.BookDto"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Name.</param>
        /// <param name="genreId">Genre identifier.</param>
        /// <param name="genre">Genre.</param>
        /// <param name="authors">Authors.</param>
        /// <param name="createdAt">Created at.</param>
        [JsonConstructor]
        public BookDto(Guid? id, string name, Guid genreId,
            GenreDto? genre, List<AuthorDto>? authors,
            DateTime? createdAt)
        {
            Id = id;
            Name = name;
            GenreId = genreId;
            Genre = genre;
            Authors = authors;
            CreatedAt = createdAt;
        }

        #endregion
    }
}
