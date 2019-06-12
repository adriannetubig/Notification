using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BaseData.Services
{
    public class EFRepository<TEntity> : IEFRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;

        public EFRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Create
        public async Task Create(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region Read
        public async Task<TEntity> Read(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(expression, cancellationToken);
        }
        public IQueryable<TEntity> ReadAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }
        #endregion

        #region Update
        public async Task Update(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region Delete
        #endregion
    }
}
