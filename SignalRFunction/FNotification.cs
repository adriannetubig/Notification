using AutoMapper;
using BaseModel;
using LinqKit;
using MediatR;
using SignalRData.Services;
using SignalREntity;
using SignalRModel;
using SignalRModel.Events;
using SignalRModel.Filter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRFunction
{
    public class FNotification : IFNotification
    {
        private readonly IMapper _iMapper;
        private readonly IMediator _mediator;
        private readonly IRNotification _iRNotification;
        public FNotification(IMapper iMapper, IMediator mediator, IRNotification iRNotification)
        {
            _iMapper = iMapper;
            _mediator = mediator;
            _iRNotification = iRNotification;
        }

        public async Task SendMessageToAuthenticatedConsumer(Notification notification, CancellationToken cancellationToken)
        {
            await SendMessageToAuthenticatedConsumer(notification, cancellationToken, 0);

        }

        public async Task SendMessageToAuthenticatedConsumer(Notification notification, CancellationToken cancellationToken, int createdBy)
        {
            var authenticatedHubEvent = new AuthenticatedHubEvent
            {
                Sender = notification.Sender,
                Message = notification.Message
            };

            var eNotification = _iMapper.Map<ENotification>(notification);
            eNotification.CreatedBy = createdBy;

            await _iRNotification.Create(eNotification, cancellationToken);
            await _mediator.Publish(authenticatedHubEvent, cancellationToken);
        }

        public async Task SendMessageToUnauthenticatedConsumer(Notification notification, CancellationToken cancellationToken)
        {
            await SendMessageToUnauthenticatedConsumer(notification, cancellationToken, 0);
        }

        public async Task SendMessageToUnauthenticatedConsumer(Notification notification, CancellationToken cancellationToken, int createdBy)
        {
            var unauthenticatedHubEvent = new UnauthenticatedHubEvent
            {
                Sender = notification.Sender,
                Message = notification.Message
            };

            var eNotification = _iMapper.Map<ENotification>(notification);
            eNotification.CreatedBy = createdBy;

            await _iRNotification.Create(eNotification, cancellationToken);
            //demo for how regular EF calls work
            eNotification.NotificationId = 0;
            await _iRNotification.RedundantAdd(eNotification);
            await _mediator.Publish(unauthenticatedHubEvent, cancellationToken);
        }

        public async Task<RequestResult<List<Notification>>> Read(CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<List<Notification>>();
            try
            {
                var eNotifications = await _iRNotification.ReadMultiple(a => true, cancellationToken);
                requestResult.Model = _iMapper.Map<List<Notification>>(eNotifications);
            }
            catch (Exception e)
            {
                requestResult.Exceptions.Add(e);
            }
            return requestResult;
        }

        public async Task<RequestResult<List<Notification>>> Read(NotificationFilter notificationFilter, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<List<Notification>>();
            try
            {
                var filterModel = notificationFilter.FilterModel;

                Expression<Func<ENotification, bool>> predicate = PredicateBuilder.New<ENotification>();
                predicate = a => true;

                if (!string.IsNullOrEmpty(filterModel.Sender))
                    predicate = predicate.And(a => a.Sender.Contains(filterModel.Sender));

                if (!string.IsNullOrEmpty(filterModel.Message))
                    predicate = predicate.And(a => a.Message.Contains(filterModel.Message));

                var eNotifications = await _iRNotification.ReadMultiple(predicate, cancellationToken);
                requestResult.Model = _iMapper.Map<List<Notification>>(eNotifications);
            }
            catch (Exception e)
            {
                requestResult.Exceptions.Add(e);
            }
            return requestResult;
        }

        public async Task<RequestResult> Update(Notification notification, CancellationToken cancellationToken, int updatedBy)
        {
            var requestResult = new RequestResult();
            try
            {
                //Prevent updating other fields
                var eNotification = await _iRNotification.ReadSingle(a => a.NotificationId == notification.NotificationId, cancellationToken);

                eNotification.UpdatedBy = updatedBy;
                eNotification.Message = notification.Message;
                eNotification.UpdatedDateUtc = DateTime.UtcNow;

                await _iRNotification.Update(eNotification, cancellationToken);
            }
            catch (Exception e)
            {
                requestResult.Exceptions.Add(e);
            }
            return requestResult;
        }

        public async Task<RequestResult> Delete(int notificationId, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult();
            try
            {
                await _iRNotification.Delete(a => a.NotificationId == notificationId, cancellationToken);
            }
            catch (Exception e)
            {
                requestResult.Exceptions.Add(e);
            }
            return requestResult;
        }
    }
}
