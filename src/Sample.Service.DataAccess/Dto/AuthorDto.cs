using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Sample.Service.DataAccess.Interfaces;

namespace Sample.Service.DataAccess.Dto
{
    /// <summary>
    /// Author DTO.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class AuthorDto : IDto
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
        /// Gets the author books.
        /// </summary>
        public List<BookDto>? Books { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        public DateTime? CreatedAt { get; set; }

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.DataAccess.Dto.AuthorDto"/> class.
        /// </summary>
        public AuthorDto()
        {
            Name = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.DataAccess.Dto.AuthorDto"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="books"></param>
        /// <param name="createdAt"></param>
        [JsonConstructor]
        public AuthorDto(Guid? id, string name, List<BookDto>? books,
            DateTime? createdAt)
        {
            Id = id;
            Name = name;
            Books = books;
            CreatedAt = createdAt;
        }

        #endregion
    }
}
