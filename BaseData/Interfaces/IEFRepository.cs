using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BaseData.Services
{
    public interface IEFRepository<TEntity> where TEntity : class
    {

        #region Create
        Task Create(TEntity entity, CancellationToken cancellationToken);
        #endregion

        #region Read
        Task<TEntity> Read(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        IQueryable<TEntity> ReadAll();
        #endregion

        #region Update
        Task Update(TEntity entity, CancellationToken cancellationToken);
        #endregion

        #region Delete
        #endregion
    }
}
