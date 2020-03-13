using System;
namespace Sample.Service.DataAccess.Interfaces
{
    /// <summary>
    /// Interface for Dtos
    /// </summary>
    public interface IDto
    {
        #region :: Properties ::

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        Guid? Id { get; }

        #endregion
    }
}
