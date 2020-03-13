using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Sample.Service.Models.Common;
using Sample.Service.Models.Interfaces;
using Sample.Service.Models.Models;

namespace Sample.Service.Models
{
    /// <summary>
    /// Sample DbContext.
    /// </summary>
    public class SampleDbContext : DbContext
    {
        #region :: Properties ::

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>The books.</value>
        public DbSet<Author> Authors { get; set; } = null!;

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>The books.</value>
        public DbSet<Book> Books { get; set; } = null!;

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>The books.</value>
        public DbSet<AuthorBook> AuthorsBooks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the genres.
        /// </summary>
        /// <value>The genres.</value>
        public DbSet<Genre> Genres { get; set; } = null!;

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.Models.SampleDbContext"/> class.
        /// </summary>
        /// <param name="options">Options.</param>
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options) { }

        #endregion

        #region :: Methods ::

        /// <summary>
        /// Adds the timestamps.
        /// </summary>
        private void AddTimestamps(EntityEntry changedEntity)
        {
            var now = DateTime.UtcNow;

            IEntity? entity = changedEntity.Entity as IEntity;
            if (entity != null)
            {
                if (changedEntity.State.Equals(EntityState.Added))
                {
                    entity.CreatedAt = now;
                    entity.UpdatedAt = now;
                }
                else if (changedEntity.State.Equals(EntityState.Modified)
                || changedEntity.State.Equals(EntityState.Deleted))
                {
                    Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                    entity.UpdatedAt = now;
                }
            }
        }

        /// <summary>
        /// Adjust the property IsDeleted of the entity according to its entity state.
        /// </summary>
        private void OnBeforeSaving()
        {
            ChangeTracker.Entries().ToList().ForEach(delegate (EntityEntry entry)
            {
                AddTimestamps(entry);
                if (entry.State == EntityState.Added)
                {
                    entry.CurrentValues["IsDeleted"] = false;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsDeleted"] = true;
                }
            });
        }

        #region  Overrided Methods

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        /// <returns>The changes async.</returns>
        /// <param name="cancellationToken">Cancellation token.</param>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        /// <returns>The changes async.</returns>
        public Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(new CancellationToken());
        }

        /// <summary>
        /// Apply the correct rules for every model.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasQueryFilter(cr => !cr.IsDeleted);

            modelBuilder.Entity<Book>()
                .HasQueryFilter(cr => !cr.IsDeleted);

            modelBuilder.Entity<Genre>()
                .HasQueryFilter(cr => !cr.IsDeleted);

            modelBuilder.Entity<Genre>()
                .HasMany(cr => cr.Books)
                .WithOne(o => o.Genre)
                .HasForeignKey(o => o.GenreId);

            modelBuilder.Entity<AuthorBook>()
                .HasQueryFilter(ab => !ab.IsDeleted);

            modelBuilder.Entity<AuthorBook>()
                .HasKey(ab => new { ab.AuthorId, ab.BookId });

            modelBuilder.Entity<AuthorBook>()
                .HasOne(pt => pt.Book)
                .WithMany(p => p.AuthorBooks)
                .HasForeignKey(pt => pt.BookId);

            modelBuilder.Entity<AuthorBook>()
                .HasOne(pt => pt.Author)
                .WithMany(t => t.AuthorBooks)
                .HasForeignKey(pt => pt.AuthorId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Model.GetEntityTypes().ToList().ForEach(delegate (IMutableEntityType entity)
            {
                // Replace table names
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                // Replace column names
                entity.GetProperties().ToList().ForEach(property
                    => property.SetColumnName(property.GetColumnName().ToSnakeCase()));

                // Replace keys
                entity.GetKeys().ToList().ForEach(key
                    => key.SetName(key.GetName().ToSnakeCase()));

                entity.GetForeignKeys().ToList().ForEach(key
                    => key.SetConstraintName(key.GetConstraintName().ToSnakeCase()));

                // Replace indexes
                entity.GetIndexes().ToList().ForEach(index
                    => index.SetName(index.GetName().ToSnakeCase()));
            });
        }

        #endregion

        #endregion
    }
}
