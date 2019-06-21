using BaseData.Services;
using Microsoft.EntityFrameworkCore;
using SignalREntity;
using System.Threading.Tasks;

namespace SignalRData.Services
{
    public class RNotification : EFRepository<ENotification>, IRNotification
    {
        private readonly DbContextOptions<Context> _dbContextOptions;
        public RNotification(DbContext dbContext, DbContextOptions<Context> dbContextOptions) : base(dbContext)
        {
            _dbContextOptions = dbContextOptions;
        }

        public async Task RedundantAdd(ENotification eNotification)
        {
            using (var context = new Context(_dbContextOptions))
            {
                await context.AddAsync(eNotification);
                await context.SaveChangesAsync();
            }
        }
    }
}
