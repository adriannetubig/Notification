using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BaseData.Services
{
    public interface IEFRepository<TEntity> where TEntity : class
    {
        Task Create(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity> ReadSingle(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<List<TEntity>> ReadMultiple(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);

        Task Update(TEntity entity, CancellationToken cancellationToken);

        Task Delete(TEntity entity, CancellationToken cancellationToken);
        Task Delete(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
    }
}
