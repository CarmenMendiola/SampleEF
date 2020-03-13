using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Sample.Service.DataAccess.Interfaces;

namespace Sample.Service.DataAccess.Dto
{
    /// <summary>
    /// Genre DTO.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class GenreDto : IDto
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
        /// Gets the books.
        /// </summary>
        /// <value>The books.</value>
        public List<BookDto>? Books { get; private set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        public DateTime? CreatedAt { get; set; }

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.DataAccess.Dto.GenreDto"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Name.</param>
        /// <param name="books">Books.</param>
        /// <param name="createdAt">Created at</param>
        [JsonConstructor]
        public GenreDto(Guid? id, string name,
            List<BookDto>? books, DateTime? createdAt)
        {
            Id = id;
            Name = name;
            Books = books;
            CreatedAt = createdAt;
        }

        #endregion
    }
}
