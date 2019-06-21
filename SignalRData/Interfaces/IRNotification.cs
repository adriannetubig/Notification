using BaseData.Services;
using SignalREntity;
using System.Threading.Tasks;

namespace SignalRData.Services
{
    public interface IRNotification : IEFRepository<ENotification>
    {
        Task RedundantAdd(ENotification eNotification);
    }
}
