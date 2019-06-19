using BaseModel;
using SignalRModel;
using SignalRModel.Filter;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRFunction
{
    public interface IFNotification
    {
        Task SendMessageToAuthenticatedConsumer(Notification notification, CancellationToken cancellationToken);
        Task SendMessageToAuthenticatedConsumer(Notification notification, CancellationToken cancellationToken, int createdBy);
        Task SendMessageToUnauthenticatedConsumer(Notification notification, CancellationToken cancellationToken);
        Task SendMessageToUnauthenticatedConsumer(Notification notification, CancellationToken cancellationToken, int createdBy);

        Task<RequestResult<List<Notification>>> Read(CancellationToken cancellationToken);
        Task<RequestResult<List<Notification>>> Read(NotificationFilter notificationFilter, CancellationToken cancellationToken);

        Task<RequestResult> Update(Notification notification, CancellationToken cancellationToken, int createdBy);

        Task<RequestResult> Delete(int notificationId, CancellationToken cancellationToken);

    }
}
