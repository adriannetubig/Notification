import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as signalR from "@aspnet/signalr";
@Component({
  selector: 'app-unauthenticated',
  templateUrl: './unauthenticated.component.html'
})
export class UnauthenticatedComponent {
  public forecasts: WeatherForecast[];
  public Notifications: Notification[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<WeatherForecast[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
  displayedColumns: string[] = ['dateFormatted', 'temperatureC', 'temperatureF', 'summary'];
  notificationColumns: string[] = ['eventDate', 'message', 'sender'];

  ngOnInit() {
    const connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:40902/unauthenticatedHub").build();
    connection.start().catch(err => document.write(err));
    this.Notifications = [];
    connection.on("UnauthorizedMessage", (notification) => {
      this.Notifications.push(notification);
      console.log(this.Notifications);
    });

  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface Notification {
  eventDate: Date;
  message: string;
  sender: string;
}
