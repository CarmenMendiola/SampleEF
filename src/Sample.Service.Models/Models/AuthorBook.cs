using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sample.Service.Models.Interfaces;

namespace Sample.Service.Models.Models
{
    /// <summary>
    /// Author book.
    /// </summary>
    public class AuthorBook : IEntity
    {
        #region :: Properties ::

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the book identifier.
        /// </summary>
        /// <value>The book identifier.</value>
        public Guid BookId { get; set; }

        /// <summary>
        /// Gets the book.
        /// </summary>
        /// <value>The book.</value>
        public Book Book { get; set; }

        /// <summary>
        /// Gets the author identifier.
        /// </summary>
        /// <value>The author identifier.</value>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Gets the author.
        /// </summary>
        /// <value>The author.</value>
        public Author Author { get; set; }

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
        public AuthorBook()
        {
            Id = Guid.Empty;
            AuthorId = Guid.Empty;
            Author = new Author();
            BookId = Guid.Empty;
            Book = new Book();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.Models.Models.Author"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="authorId">Author identifier.</param>
        /// <param name="author">Author.</param>
        /// <param name="bookId">Book identidier.</param>
        /// <param name="book">Book.</param>
        public AuthorBook(Guid id, Guid authorId, Author author,
            Guid bookId, Book book)
        {
            Id = id;
            AuthorId = authorId;
            Author = author;
            BookId = bookId;
            Book = book;
        }

        #endregion
    }
}
