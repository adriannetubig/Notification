"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:40902/notificationHub", {
    accessTokenFactory: () =>
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiam9obmRvZSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik1hbmFnZXIiLCJuYmYiOjE1NTM3NDE2MTUsImV4cCI6MTU1Mzc0MTY3NSwiaXNzIjoiSXNzdWVyIiwiYXVkIjoiQXVkaWVuY2UifQ.2HBSiii7sJAXg7dC55e5cKIUU1-TxYEAQphqzw0s85w"
})
    .build();
//var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:40902/notificationHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("WithAuthorization", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    //connection.invoke("WithoutAuthorization", user, message).catch(function (err) {
    //    return console.error(err.toString());
    //});
    event.preventDefault();
});