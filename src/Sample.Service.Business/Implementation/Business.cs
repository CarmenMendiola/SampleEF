using System;
using System.Threading.Tasks;
using Sample.Service.Business.Exceptions;
using Sample.Service.Business.Interfaces;
using Sample.Service.DataAccess.Dto.Common;
using Sample.Service.DataAccess.Interfaces;
using Sample.Service.DataAccess.Interfaces.Repositories;
using Sample.Service.Models.Filters;
using Sample.Service.Models.Interfaces;

namespace Sample.Service.Business.Implementation
{
    /// <summary>
    /// Business.
    /// </summary>
    public class Business<T, U> : IBusiness<T, U>
        where T : class, IEntity
        where U : class, IDto
    {
        #region :: Fields ::

        /// <summary>
        /// The repository.
        /// </summary>
        protected readonly IRepository<T, U> repository;

        #endregion

        #region  Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.Business.Implementation.Business"/> class.
        /// </summary>
        /// <param name="repository">Repository.</param>
        public Business(IRepository<T, U> repository)
        {
            this.repository = repository;
        }

        #endregion

        #region :: Methods ::

        /// <summary>
        /// Adds an entity async.
        /// </summary>
        /// <returns>The Maybe entity.</returns>
        /// <param name="entity">Entity.</param>
        public async Task<U?> AddAsync(U? entity)
            => await repository.AddAsync(entity).ConfigureAwait(false);

        /// <summary>
        /// Deletes an entity async.
        /// </summary>
        /// <returns>The Maybe entity.</returns>
        /// <param name="id">Entity identifier.</param>
        public async Task DeleteAsync(Guid id)
        {
            var entity = await repository.FindByIdModelAsync(id).ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            await repository.DeleteAsync(entity).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates an entity async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="entity">Entity.</param>
        /// <param name="entityId">Entity identifier.</param>
        public async Task UpdateAsync(U? entity, Guid entityId)
        {
            if (entity != null && entityId != entity.Id)
            {
                throw new NotFoundException();
            }

            var entityFound = await repository.FindByIdModelAsync(entityId)
                .ConfigureAwait(false);

            if (entityFound == null)
            {
                throw new NotFoundException();
            }

            await repository.UpdateAsync(entity).ConfigureAwait(false);
        }

        /// <summary>
        /// Finds the model entity by identifier async.
        /// </summary>
        /// <returns>The model by identifier async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<T?> FindByIdModelAsync(Guid id)
            => await repository.FindByIdModelAsync(id).ConfigureAwait(false);

        /// <summary>
        /// Finds the entities of the by type.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <returns>The by type.</returns>
        public async Task<ListDto<U>> FindAllAsync(FilterModelBase filter)
            => await repository.FindAllAsync(filter).ConfigureAwait(false);

        /// <summary>
        /// Finds the entity by identifier async.
        /// </summary>
        /// <returns>The complete by identifier async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<U?> FindByIdAsync(Guid id)
            => await repository.FindByIdAsync(id).ConfigureAwait(false);

        #endregion
    }
}
