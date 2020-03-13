using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sample.Service.Models.Interfaces;

namespace Sample.Service.Models.Models
{
    /// <summary>
    /// Book genre.
    /// </summary>
    public class Genre : IEntity
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
        /// Gets the books.
        /// </summary>
        /// <value>The books.</value>
        public List<Book> Books { get; private set; }

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
        /// Initializes a new instance of the <see cref="T:Sample.Service.Models.Models.Genre"/> class.
        /// </summary>
        public Genre()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            Books = new List<Book>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.Models.Models.Genre"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Name.</param>
        /// <param name="books">Books.</param>
        public Genre(Guid id, string name, List<Book> books)
        {
            Id = id;
            Name = name;
            Books = books;
        }

        #endregion
    }
}
