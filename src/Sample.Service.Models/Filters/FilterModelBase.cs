using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Service.Models.Filters
{
    /// <summary>
    /// Filter the base model.
    /// </summary>
    public class FilterModelBase
    {
        #region :: Properties ::

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>The page number.</value>
        [Range(0, int.MaxValue)]
        public int page { get; set; }

        /// <summary>
        /// Gets or sets the limit of entities to return.
        /// </summary>
        /// <value>The limit.</value>
        [Range(0, int.MaxValue)]
        public int limit { get; set; }

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Checkout.Service.Models.Filters.FilterModelBase"/> class.
        /// </summary>
        public FilterModelBase()
        {
            page = 1;
            limit = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Checkout.Service.Models.Filters.FilterModelBase"/> class.
        /// </summary>
        /// <param name="limit">Limit.</param>
        public FilterModelBase(int limit)
        {
            page = 1;
            this.limit = limit;
        }

        #endregion
    }
}
