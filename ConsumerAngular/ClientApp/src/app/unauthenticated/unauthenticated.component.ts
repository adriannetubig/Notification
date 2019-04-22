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
  private reconnectionAttempt: number;
  private reconnectionAttemptDelaySeconds: number;
  notificationColumns: string[] = ['eventDate', 'message', 'sender'];
  dataSource = new MatTableDataSource(this._notifications);
  connection: any;

  constructor(private _ngZone: NgZone) {

  }

  ngOnInit() {
    this.reconnectionAttempt = Number(config.notification.reconnectionAttempts);
    this.reconnectionAttemptDelaySeconds = Number(config.notification.reconnectionAttemptDelaySeconds) * 1000;
    this._notifications = [];
    this.ConnectToHub();

  }

  private ConnectToHub() {
    try {
      if (this.connection == null || this.connection.state == 0) {

        this.connection = new HubConnectionBuilder().withUrl(config.notification.url + "/unauthenticatedHub").build();

        this.connection.start().catch(err => console.log(err));

        this.connection.on("UnauthorizedMessage", (notification) => {
          this._notifications.push(notification);
          this._ngZone.run(() => {
            this.dataSource.data = this._notifications;
          });
        });

        this.connection.onclose(() => {
          this.Reconnect(0);
        });

      }
    }
    catch (e) {
      console.log(e);
    }
  }

  Reconnect(iteration: number) {
    iteration += 1;

      this.ConnectToHub();
      if ((iteration < this.reconnectionAttempt || this.reconnectionAttempt === 0) && this.connection.state === 0)
        setTimeout(() => { this.Reconnect(iteration); }, this.reconnectionAttemptDelaySeconds * iteration);
  }
}


