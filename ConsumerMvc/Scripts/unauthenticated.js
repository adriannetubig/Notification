"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:40902/unauthenticatedHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("AuthorizedMessage", function (notification) {
    var encodedMsg = notification.eventDate + " " + notification.sender + " : " + notification.message;
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

    event.preventDefault();
});