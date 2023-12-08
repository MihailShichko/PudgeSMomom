"use strict"

let connection = new signalR.HubConnectionBuilder().withUrl("/ChatRoom").build();

document.getElementById("sendButton").disabled = false;
document.getElementById("sendButton").addEventListener("click", function (event) {
    let user = document.getElementById("userInput").value
    let message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    }
});

connection.on("Receive", function (user, message) {

    let li = document.createElement("li");
  
    document.getElementById("messageList").appendChild(li);
    li.textContent = '${user} says ${message}'; 
});

connection.start()
    .then(function () {
        document.getElementById("sendButton").disabled = false;
    })
    .catch(function (err) { 
        return console.error(err.toString());
    });