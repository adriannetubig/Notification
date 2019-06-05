import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import * as config from '../../assets/appsettings.json';
import { Notification } from '../../shared/models/Notification';

@Injectable({
  providedIn: 'root'
})

export class UnauthenticatedService {
  constructor(private http: HttpClient) {
  }

  Send(notification: Notification) {
    return this.http.post(config.notification.url + '/api/Notifications/SendMessageToUnauthenticatedConsumer', notification);
  }
}


