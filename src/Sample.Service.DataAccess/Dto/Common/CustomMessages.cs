using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Sample.Service.DataAccess.Dto.Common
{
    /// <summary>
    /// Class with custom messages.
    /// </summary>
    [Serializable]
    public class CustomMessages : Dictionary<string, string>
    {
        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.DataAccess.Dto.Common.CustomMessages"/> class.
        /// </summary>
        public CustomMessages()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.DataAccess.Dto.Common.CustomMessages"/> class.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        [ExcludeFromCodeCoverage]
        protected CustomMessages(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion
    }
}
