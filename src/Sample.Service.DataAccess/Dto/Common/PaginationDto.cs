using Newtonsoft.Json;

namespace Sample.Service.DataAccess.Dto.Common
{
    /// <summary>
    /// Pagination DTO.
    /// </summary>
    public class PaginationDto
    {
        #region :: Properties ::

        /// <summary>
        /// Gets the page index.
        /// </summary>
        /// <value>The page index.</value>
        public int PageIndex { get; private set; }

        /// <summary>
        /// Gets the total pages.
        /// </summary>
        /// <value>The total pages.</value>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <value>The total items.</value>
        public int TotalItems { get; private set; }

        /// <summary>
        /// Gets or sets the items per page.
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Gets the has previous page.
        /// </summary>
        /// <value>The has previous page.</value>
        public bool HasPreviousPage { get { return PageIndex > 1; } }

        /// <summary>
        /// Gets the has next page.
        /// </summary>
        /// <value>The has next page.</value>
        public bool HasNextPage { get { return PageIndex < TotalPages; } }

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.DataAccess.Dto.Common.PaginationDto`1"/> class.
        /// </summary>
        /// <param name="itemsPerPage">Items per page.</param>
        /// <param name="count">Count.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="totalPages">Total pages.</param>
        [JsonConstructor]
        public PaginationDto(int itemsPerPage, int count, int pageIndex, int totalPages)
        {
            ItemsPerPage = itemsPerPage;
            TotalItems = count;
            PageIndex = pageIndex;
            TotalPages = totalPages;
        }

        #endregion
    }
}
