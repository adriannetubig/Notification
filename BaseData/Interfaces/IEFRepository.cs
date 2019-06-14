using System;
using System.Collections.Generic;
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
        Task<TEntity> ReadSingle(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<List<TEntity>> ReadMultiple(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        #endregion

        #region Update
        //Task Update(TEntity entity, CancellationToken cancellationToken);
        #endregion

        #region Delete
        #endregion
    }
}
