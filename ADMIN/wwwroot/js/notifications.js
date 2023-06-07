"use strict";

// Shafi: Create  connection
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/notifications")
    .build();
// End:

// Shafi: Connect to connection
connection.on("SendNotification", function (message, amount) {
    alert("Product name is " + message + " and amount is " + amount);
});
// End:

// Shafi: Connection Start
connection.start().then(function () { 
}).catch(function (err) {
    return console.error(err.toString());
});
// End:

// Shafi: Begin Connection
document.getElementById("pushButton").addEventListener("click", function (event) {
    connection.invoke("Push").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
// End: