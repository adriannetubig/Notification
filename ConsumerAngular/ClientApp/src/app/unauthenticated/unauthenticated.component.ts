import { Component, NgZone } from '@angular/core';
import { HubConnectionBuilder } from "@aspnet/signalr";
import { MatTableDataSource } from '@angular/material';
import * as config from '../../assets/appsettings.json';
import { Notification } from '../../shared/models/Notification';

@Component({
  selector: 'app-unauthenticated',
  templateUrl: './unauthenticated.component.html'
})

export class UnauthenticatedComponent {
  private _notifications: Notification[];

  constructor(private _ngZone: NgZone) {

  }

  notificationColumns: string[] = ['eventDate', 'message', 'sender'];
  dataSource = new MatTableDataSource(this._notifications);

  ngOnInit() {
    this._notifications = [];
    this.ConnectToHub();
  }

  private ConnectToHub() {
    const connection = new HubConnectionBuilder().withUrl(config.notification.url + "/unauthenticatedHub").build();
    connection.start().catch(err => document.write(err));

    connection.on("UnauthorizedMessage", (notification) => {
      this._notifications.push(notification);
      this._ngZone.run(() => {
        this.dataSource.data = this._notifications;
      });
    });
  }
}


