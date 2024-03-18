using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moneyon.Common.Data.SqlServer;
using Moneyon.PowerBi.Domain.Model.Modeling;
using System.Linq.Expressions;

namespace Infrastructure.Data
{
    public class GenericRepository<TEntity> : SqlServerGenericRepository<TEntity, long>, IGenericRepository<TEntity> 
        where TEntity : AppEntity
    {
        public DbContext db { get; }
        internal DbSet<TEntity> dbSet;
        public GenericRepository(DbContext context) : base(context)
        {
            db = context;
            dbSet = db.Set<TEntity>();
        }

        public async Task Update(TEntity entity)
        {
            var t = await dbSet.FindAsync(entity.Id);
            t = entity;
        }

        public async Task<long> CountWithDistinctAsync(Expression<Func<TEntity, bool>>? filter = null, IEqualityComparer<TEntity>? comparer = null)
        {
            if (filter is null && comparer is null)
                return await dbSet.LongCountAsync();
            else if (filter is not null && comparer is null)
                return await dbSet.LongCountAsync(filter);
            else if (filter is null && comparer is not null)
                return await dbSet.Distinct(comparer).LongCountAsync();
            else
                return await dbSet.Distinct(comparer).LongCountAsync(filter);
        }

        public async Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter,
                                                                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                                                    int recordCount = 0,
                                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            if (recordCount == 0)
                return await ReadAsync(filter, include, orderBy, cancellationToken);

            return await orderBy(include(dbSet.AsQueryable().Where(filter))).Take(recordCount).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter,
                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                                    int recordCount = 0,
                                                    CancellationToken cancellationToken = default)
        {
            if (recordCount == 0)
                return await ReadAsync(filter: filter, orderBy: orderBy, cancellationToken);

            return await orderBy(dbSet.AsQueryable().Where(filter))?.Take(recordCount)?.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter,
                                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                                                          int recordCount = 0,
                                                          CancellationToken cancellationToken = default)
        {
            if (recordCount == 0)
                return await ReadAsync(filter, include, cancellationToken);

            return await include(dbSet.AsQueryable().Where(filter)?.Take(recordCount))?.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ReadAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                                          int recordCount = 0,
                                                          CancellationToken cancellationToken = default)
        {
            if (recordCount == 0)
                return await ReadAsync(include, orderBy, cancellationToken);

            return await orderBy(include(dbSet.AsQueryable()))?.Take(recordCount)?.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter,
                                                    int recordCount = 0,
                                                    CancellationToken cancellationToken = default)
        {
            if (recordCount == 0)
                return await ReadAsync(filter, cancellationToken);

            return await (dbSet.AsQueryable().Where(filter)?.Take(recordCount))?.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ReadAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                                                    int recordCount = 0,
                                                    CancellationToken cancellationToken = default)
        {
            if (recordCount == 0)
                return await ReadAsync(include, cancellationToken);

            return await include(dbSet.AsQueryable()?.Take(recordCount))?.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ReadAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                                    int recordCount = 0,
                                                    CancellationToken cancellationToken = default)
        {
            if (recordCount == 0)
                return await ReadAsync(orderBy, cancellationToken);

            return await orderBy(dbSet.AsQueryable())?.Take(recordCount)?.ToListAsync(cancellationToken);
        }
    }
}
