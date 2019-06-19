using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task Create(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> ReadSingle(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(expression, cancellationToken);
        }
        public async Task<List<TEntity>> ReadMultiple (Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(expression)
                .ToListAsync(cancellationToken);
        }

        public async Task Update(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task Delete(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            var entity = await ReadSingle(expression, cancellationToken);
            await Delete(entity, cancellationToken);
        }
    }
}
