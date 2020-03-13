using System;
using System.Threading.Tasks;
using Sample.Service.DataAccess.Dto.Common;
using Sample.Service.DataAccess.Interfaces;
using Sample.Service.Models.Filters;
using Sample.Service.Models.Interfaces;

namespace Sample.Service.Business.Interfaces
{
    /// <summary>
    /// Business interface.
    /// </summary>
    /// <typeparam name="T">Model type.</typeparam>
    /// <typeparam name="U">Dto type.</typeparam>
    public interface IBusiness<T, U>
        where T : class, IEntity
        where U : class, IDto
    {
        #region :: Methods ::

        /// <summary>
        /// Adds an entity async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="entity">Entity.</param>
        Task<U?> AddAsync(U? entity);

        /// <summary>
        /// Deletes an entity async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="id">Entity identifier.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Updates an entity async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="entity">Entity.</param>
        /// <param name="entityId">Entity identifier.</param>
        Task UpdateAsync(U? entity, Guid entityId);


        /// <summary>
        /// Finds the entities of the by type.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <returns>The by type.</returns>
        Task<ListDto<U>> FindAllAsync(FilterModelBase filter);

        /// <summary>
        /// Finds the entity by identifier async.
        /// </summary>
        /// <returns>The complete by identifier async.</returns>
        /// <param name="id">Identifier.</param>
        Task<U?> FindByIdAsync(Guid id);

        #endregion
    }
}
