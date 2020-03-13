using System;
using System.Threading.Tasks;
using Sample.Service.DataAccess.Dto.Common;
using Sample.Service.Models.Filters;
using Sample.Service.Models.Interfaces;

namespace Sample.Service.DataAccess.Interfaces.Repositories
{
    /// <summary>
    /// Repository.
    /// <typeparamref name="T">Model class.</typeparamref>
    /// <typeparamref name="U">DTO class.</typeparamref>
    /// </summary>
    public interface IRepository<T, U>
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
        /// Delete the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        Task DeleteAsync(T? entity);

        /// <summary>
        /// Update the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        Task UpdateAsync(U? entity);

        /// <summary>
        /// Finds the by identifier async.
        /// </summary>
        /// <returns>The by identifier async.</returns>
        /// <param name="id">Identifier.</param>
        Task<T?> FindByIdModelAsync(Guid id);

        /// <summary>
        /// Finds the by identifier async.
        /// </summary>
        /// <returns>The by identifier async.</returns>
        /// <param name="id">Identifier.</param>
        Task<U?> FindByIdAsync(Guid id);

        /// <summary>
        /// Finds all async.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <returns>The all async.</returns>
        Task<ListDto<U>> FindAllAsync(FilterModelBase filter);

        #endregion
    }
}
