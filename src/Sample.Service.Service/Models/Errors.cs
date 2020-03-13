using System.Diagnostics.CodeAnalysis;

namespace Sample.Service.Service.Models
{
    /// <summary>
    /// Errors.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Errors
    {
        #region :: Properties ::

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string? type { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string? message { get; set; }

        /// <summary>
        /// Gets or sets the http statuscode.
        /// </summary>
        /// <value>The http statuscode.</value>
        public int httpStatuscode { get; set; }

        #endregion
    }
}
