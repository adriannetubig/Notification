using BaseModel;
using SignalRModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRFunction
{
    public interface IFNotification
    {
        #region Create
        Task SendMessageToAuthenticatedConsumer(Notification notification, CancellationToken cancellationToken);
        Task SendMessageToUnauthenticatedConsumer(Notification notification, CancellationToken cancellationToken);
        #endregion

        #region Read
        Task<RequestResult<List<Notification>>> Read(CancellationToken cancellationToken);
        #endregion
    }
}
