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
        Task SendMessageToAuthenticatedConsumer(Notification notification, CancellationToken cancellationToken, int createdBy);
        Task SendMessageToUnauthenticatedConsumer(Notification notification, CancellationToken cancellationToken);
        Task SendMessageToUnauthenticatedConsumer(Notification notification, CancellationToken cancellationToken, int createdBy);
        #endregion

        #region Read
        Task<RequestResult<List<Notification>>> Read(CancellationToken cancellationToken);
        #endregion

        #region Update
        Task<RequestResult> Update(Notification notification, CancellationToken cancellationToken, int createdBy);
        #endregion

        #region Delete
        Task<RequestResult> Delete(int notificationId, CancellationToken cancellationToken);
        #endregion
    }
}
