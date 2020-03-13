using System.Collections.Generic;
using Newtonsoft.Json;
using Sample.Service.DataAccess.Interfaces;

namespace Sample.Service.DataAccess.Dto.Common
{
    /// <summary>
    /// How lists will be returned in the api
    /// </summary>
    public class ListDto<T> where T : class, IDto
    {
        #region :: Properties ::

        /// <summary>
        /// Gets the data collection.
        /// </summary>
        /// <value>The data.</value>
        public IReadOnlyCollection<T>? Data { get; private set; }

        /// <summary>
        /// Gets or sets the pagination.
        /// </summary>
        /// <value>The pagination.</value>
        public PaginationDto Pagination { get; set; }

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.DataAccess.Dto.Common.ListDto`1"/> class.
        /// </summary>
        /// <param name="data">Data collection.</param>
        /// <param name="itemsPerPage">Items per page.</param>
        /// <param name="count">Count.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="totalPages">Total pages.</param>
        [JsonConstructor]
        public ListDto(IReadOnlyCollection<T>? data, int itemsPerPage, int count, int pageIndex, int totalPages)
        {
            Pagination = new PaginationDto(itemsPerPage, count, pageIndex, totalPages);
            Data = data;
        }

        #endregion
    }
}
