using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sample.Service.Service.Models
{
    /// <summary>
    /// Custom errors.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CustomErrors
    {
        #region :: Properties ::

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public List<Errors>? Errors { get; set; }

        #endregion
    }
}
