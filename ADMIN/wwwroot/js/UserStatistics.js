"use strict";

// Shafi: Create connection
var connection = new signalR.HubConnectionBuilder().withUrl("/userstatistics").build();
// End:

// Shafi: When connection is on executive this
connection.on("updateCount", function (Count) {

    $("#Shafi").html("Users Online " + Count);

    var subject = "Users Online " + Count;

    $.notify(
        subject,
        {
            autoHide: true,
            autoHideDelay: 5000,
            className: 'success'
        }
    );
});
// End:

// Shafi: Start connection
connection.start().then(function () {
}).catch(function (err) {
    console.log(err.toString());
});
// End:

// Shafi: Invoke connection
$(document).ready(function () {
    connection.invoke("OnConnectedAsync", user, message).catch(function (err) {
        console.log(error.toString());
    });
});

//$("#pushButton").click(function () {
//    var user = $("#txtUser").val();
//    var message = $("#txtMessage").val();
//    connection.invoke("OnConnectedAsync", user, message).catch(function (err) {
//        console.log(error.toString());
//    });
//});
// End:


// Shafi: Invoke connection
//$("#pushButton").click(function () {
//    var user = $("#txtUser").val();
//    var message = $("#txtMessage").val();
//    connection.invoke("OnConnectedAsync", user, message).catch(function (err) {
//        console.log(error.toString());
//    });
//});
// End: