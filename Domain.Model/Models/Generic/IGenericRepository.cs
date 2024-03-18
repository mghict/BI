using Microsoft.EntityFrameworkCore.Query;
using Moneyon.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moneyon.PowerBi.Domain.Model.Modeling;

public interface IGenericRepository<TEntity> : IGenericRelationalRepository<TEntity, long> where TEntity : AppEntity
{
    Task Update(TEntity entity);

    Task<long> CountWithDistinctAsync(Expression<Func<TEntity, bool>>? filter = null, IEqualityComparer<TEntity>? comparer = null);
    Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter,
                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                            int recordCount = 0,
                                            CancellationToken cancellationToken = default(CancellationToken));

    Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                            int recordCount = 0,
                                            CancellationToken cancellationToken = default(CancellationToken));

    Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter,
                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                                            int recordCount = 0,
                                            CancellationToken cancellationToken = default(CancellationToken));

    Task<IEnumerable<TEntity>> ReadAsync(
                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                            int recordCount = 0,
                                            CancellationToken cancellationToken = default(CancellationToken));

    Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter,
                                            int recordCount = 0,
                                            CancellationToken cancellationToken = default(CancellationToken));

    Task<IEnumerable<TEntity>> ReadAsync(
                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                                            int recordCount = 0,
                                            CancellationToken cancellationToken = default(CancellationToken));

    Task<IEnumerable<TEntity>> ReadAsync(
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                            int recordCount = 0,
                                            CancellationToken cancellationToken = default(CancellationToken));

}
