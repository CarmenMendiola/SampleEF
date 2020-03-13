using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sample.Service.Models.Interfaces;

namespace Sample.Service.Models.Models
{
    /// <summary>
    /// Author.
    /// </summary>
    public class Author : IEntity
    {
        #region :: Properties ::

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the author books.
        /// </summary>
        public List<AuthorBook> AuthorBooks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Checkout.Service.Models.Models.Address"/> is deleted.
        /// </summary>
        /// <value><c>true</c> if is deleted; otherwise, <c>false</c>.</value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>The updated at.</value>
        public DateTime? UpdatedAt { get; set; }

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.Models.Models.Author"/> class.
        /// </summary>
        public Author()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            AuthorBooks = new List<AuthorBook>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.Models.Models.Author"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Name.</param>
        /// <param name="authorBooks">Author books.</param>
        public Author(Guid id, string name, List<AuthorBook> authorBooks)
        {
            Id = id;
            Name = name;
            AuthorBooks = authorBooks;
        }

        #endregion
    }
}
