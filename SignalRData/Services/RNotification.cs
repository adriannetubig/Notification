using BaseData.Services;
using Microsoft.EntityFrameworkCore;
using SignalREntity;

namespace SignalRData.Services
{
    public class RNotification : EFRepository<ENotification>, IRNotification
    {
        public RNotification(DbContext dbContext): base(dbContext) { }
    }
}
