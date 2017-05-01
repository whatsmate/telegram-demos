#!/usr/bin/env node

var http = require('http');

/**
 * You will need to sign up for the Enterprise plan in order for this script to work for you.
 * https://www.whatsmate.net/telegram-gateway-enterprise.html
 *
 * This is a simplistic application that demonstrates how you can receive Telegram messages
 * from your dedicated Enterprise Telegram Gateway and reply to them accordingly.
 *
 * The following diagram illustrates how messages flow between your application and your customer.
 *     .-----------------------------------------------------------------------------------------.
 *     | This application      |      The Enterprise Telegram GW      |      Customer's Telegram |
 *     '-----------------------------------------------------------------------------------------'
 * (1)                                                              <---     "Hello"
 * (2)                       <---     "Hello"
 * (3)   "Choose a product"  ---> 
 * (4)                                "Choose a product"            ---> 
 * (5)                                                              <---     "1"
 * (6)                       <---     "1"
 * (7)   "Price: 90.0 "      ---> 
 * (8)                                "Price: 90.0 "                ---> 
 */


// TCP port that this webhook application listens on
var WEBHOOK_PORT = 9999;
// Instance ID of your dedicated Enterprise Gateway
var GATEWAY_INSTANCE_ID = 99;
// Put down your own client ID and secret here:
var CLIENT_ID = "YOUR_OWN_CLIENT_ID";
var CLIENT_SECRET = "YOUR_OWN_SECRET_ID";


/**
 * User needs to write this function to define
 * how they would like to handle incoming messages.
 */
function handleMessageReceived(senderNumber, message) {
    console.log("\nReceived a message from this number: " + senderNumber)
    console.log("The message content is: " + message)

    var responseMsg = "To be defined";
    switch (message) {
      case '1':
        responseMsg = "Price: 90.0";
        break;
      case '2':
        responseMsg = "Price: 380.0";
        break;
      case '3':
        responseMsg = "Price: 990.0";
        break;
      default:
        responseMsg = "Choose a product. Send \n'1' for Product X. \n'2' for Product 'Y' and so on."
    }

    sendTelegramMessage(senderNumber, responseMsg)
}


/**
 * Sends a message to a destination number.
 */
function sendTelegramMessage(destinationNumber, message) {
    var jsonPayload = JSON.stringify({
        'number': destinationNumber,
        'message': message
    });

    var options = {
        hostname: "enterprise.whatsmate.net",
        port: 80,
        path: "/v1/telegram/single/message/" + GATEWAY_INSTANCE_ID,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-WM-CLIENT-ID": CLIENT_ID,
            "X-WM-CLIENT-SECRET": CLIENT_SECRET,
            "Content-Length": Buffer.byteLength(jsonPayload)
        }
    };

    // Call the REST API to send out the message
    var request = new http.ClientRequest(options);
    request.end(jsonPayload);

    // Define what to do with the response from the REST API call
    request.on('response', function (response) {
        console.log('Just called the REST API to send a message to ' + destinationNumber);
        console.log('Status code was ' + response.statusCode);
        response.setEncoding('utf8');
        response.on('data', function (chunk) {
            console.log('Details: ');
            console.log(chunk);
        });
    });
}


/**
 * Running a web server to capture incoming messages
 */
http.createServer(function (request, response) {
    if (request.method === "POST") {
        var requestBody = '';

        request.on('data', function(data) {
            requestBody += data;
        });

        request.on('end', function() {
            var messageObj = JSON.parse(requestBody);
            var senderNumber = messageObj['number'];
            var senderMessage = messageObj['message'];
            handleMessageReceived(senderNumber, senderMessage);

            response.writeHead(200, {'Content-Type': 'text/html'});
            response.end('Post received');
        });
    }
}).listen(WEBHOOK_PORT);


console.log('Demo application running at localhost:' + WEBHOOK_PORT);
