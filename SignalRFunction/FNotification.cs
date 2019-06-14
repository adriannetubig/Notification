﻿using AutoMapper;
using BaseModel;
using MediatR;
using SignalRData.Services;
using SignalREntity;
using SignalRModel;
using SignalRModel.Events;
using System;
using System.Collections.Generic;
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

        #region Create
        public async Task SendMessageToAuthenticatedConsumer(Notification notification, CancellationToken cancellationToken)
        {
            var authenticatedHubEvent = new AuthenticatedHubEvent
            {
                Sender = notification.Sender,
                Message = notification.Message
            };

            var eNotification = _iMapper.Map<ENotification>(notification);
            await _iRNotification.Create(eNotification, cancellationToken);
            await _mediator.Publish(authenticatedHubEvent, cancellationToken);
        }
        public async Task SendMessageToUnauthenticatedConsumer(Notification notification, CancellationToken cancellationToken)
        {
            var unauthenticatedHubEvent = new UnauthenticatedHubEvent
            {
                Sender = notification.Sender,
                Message = notification.Message
            };
            var eNotification = _iMapper.Map<ENotification>(notification);
            await _iRNotification.Create(eNotification, cancellationToken);
            await _mediator.Publish(unauthenticatedHubEvent, cancellationToken);
        }
        #endregion

        #region Read
        public async Task<RequestResult<List<Notification>>> Read(CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<List<Notification>>();
            try
            {
                var eNotifications = await _iRNotification.ReadMultiple(a => true, cancellationToken);//ToDo: add filter
            }
            catch (Exception e)
            {
                requestResult.Exceptions.Add(e);
            }
            return requestResult;
        }
        #endregion
    }
}
