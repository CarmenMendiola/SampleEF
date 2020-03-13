using System;
using System.Threading.Tasks;
using AutoMapper;
using Sample.Service.DataAccess.Interfaces;
using Sample.Service.DataAccess.Interfaces.Repositories;
using Sample.Service.Models;
using Sample.Service.Models.Interfaces;
using Sample.Service.DataAccess.Dto.Common;
using System.Collections.Generic;
using System.Linq;
using Sample.Service.Models.Filters;
using Microsoft.EntityFrameworkCore;

namespace Sample.Service.DataAccess.Implementation.Repositories
{
    /// <summary>
    /// Repository generic.
    /// </summary>
    /// <typeparamref name="T">Model class.</typeparamref>
    /// <typeparamref name="U">DTO class.</typeparamref>
    public abstract class Repository<T, U> : IRepository<T, U>
        where T : class, IEntity
        where U : class, IDto
    {
        #region :: Protected Fields ::

        /// <summary>
        /// The context.
        /// </summary>
        protected readonly SampleDbContext context;

        /// <summary>
        /// The mapper.
        /// </summary>
        protected readonly IMapper mapper;

        /// <summary>
        /// Gets the default pagination size.
        /// </summary>
        public int PaginationSize { get; set; }

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.DataAccess.Implementation.Repositories.Repository`1"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="mapper">Mapper.</param>
        /// <param name="messages">Custom messages.</param>
        protected Repository(SampleDbContext context, IMapper mapper, CustomMessages messages)
        {
            this.context = context;
            this.mapper = mapper;
            int paginationSize;
            int.TryParse(messages["MaxPaginationSize"], out paginationSize);
            PaginationSize = paginationSize;
        }

        #endregion

        #region :: Methods ::

        /// <summary>
        /// Adds an entity async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="entity">Entity.</param>
        public async Task<U?> AddAsync(U? entity)
        {
            if (entity != null)
            {
                var model = mapper.Map<T>(entity);
                return await AddModelAsync(model).ConfigureAwait(false);
            }

            return null;
        }

        /// <summary>
        /// Add the model of an entity.
        /// </summary>
        /// <param name="model">Model to add.</param>
        /// <returns>Entity added.</returns>
        protected async Task<U?> AddModelAsync(T? model)
        {
            if (model != null)
            {
                context.Set<T>().Add(model);
                context.ChangeTracker.AutoDetectChangesEnabled = false;
                await context.SaveChangesAsync().ConfigureAwait(false);
                context.Entry(model).State = EntityState.Detached;
                return Result(model);
            }

            return null;
        }

        /// <summary>
        /// Delete the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public async Task DeleteAsync(T? entity)
        {
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Update the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public async Task UpdateAsync(U? entity)
        {
            if (entity != null)
            {
                var model = mapper.Map<T>(entity);

                await UpdateModelAsync(model).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Update the specified model.
        /// </summary>
        /// <param name="model">Model to update.</param>
        /// <returns>The async.</returns>
        protected async Task UpdateModelAsync(T? model)
        {
            if (model != null)
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Finds the by identifier async.
        /// </summary>
        /// <returns>The by identifier async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<T?> FindByIdModelAsync(Guid id)
        {
            var result = await context.Set<T>().AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Finds the by identifier async.
        /// </summary>
        /// <returns>The by identifier async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<U?> FindByIdAsync(Guid id)
        {
            var result = await context.Set<T>().AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);
            return Result(result);
        }

        /// <summary>
        /// Finds all async.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <returns>The all async.</returns>
        public async Task<ListDto<U>> FindAllAsync(FilterModelBase filter)
        {
            var result = context.Set<T>().AsQueryable();

            return await Result(result).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the maybe value of the result DTO.
        /// </summary>
        /// <param name="repositoryResult">Model of result</param>
        /// <returns>Maybe value of result DTO.</returns>
        protected U? Result(T? repositoryResult)
        {
            return mapper.Map<U?>(repositoryResult);
        }

        /// <summary>
        /// Get the Maybe of the collection value in DTO.
        /// </summary>
        /// <param name="results">Model collection.</param>
        /// <param name="filter">Filter.</param>
        /// <returns>Maybe DTO collection.</returns>
        protected async Task<ListDto<U>> Result(IQueryable<T> results, FilterModelBase filter)
        {
            var count = await results.CountAsync().ConfigureAwait(false);
            var limit = filter.limit;

            if (limit.Equals(0))
            {
                limit = PaginationSize;
            }

            var totalPages = (int)Math.Ceiling((double)count / limit);
            var currentPage = filter.page;

            if (totalPages < filter.page)
            {
                currentPage = totalPages;
            }

            List<T> items = new List<T>();
            if (count > 0)
            {
                items = await results.AsNoTracking()
                        .Skip((currentPage - 1) * limit).Take(limit).ToListAsync();
            }

            return new ListDto<U>(mapper
                .Map<List<T>, List<U>>(items)
                .AsReadOnly(), limit, count, currentPage, totalPages);
        }

        /// <summary>
        /// Get the Maybe of the collection value in DTO.
        /// </summary>
        /// <param name="results">Model collection.</param>
        /// <returns>Maybe DTO collection.</returns>
        protected async Task<ListDto<U>> Result(IQueryable<T> results)
            => await Result(results, new FilterModelBase(PaginationSize)).ConfigureAwait(false);

        #endregion
    }
}
