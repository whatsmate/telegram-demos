#!/usr/bin/env node

var http = require('http');

var instanceId = "YOUR_INSTANCE_ID_HERE"; // TODO: Replace it with your gateway instance ID here
var clientId = "YOUR_CLIENT_ID_HERE";     // TODO: Replace it with your Premium Account client ID here
var clientSecret = "YOUR_CLIENT_SECRET_HERE";  // TODO: Replace it with your Premium Account client secret here

var jsonPayload = JSON.stringify({
    group_name: "Muscle Men Club",  // TODO: Specify the group name here. 
    group_admin: "19159876123",  // TODO: Specify the number of the group admin here.
    message: "Your six-pack is on the way!"
});

var options = {
    hostname: "api.whatsmate.net",
    port: 80,
    path: "/v3/telegram/group/text/message/" + instanceId,
    method: "POST",
    headers: {
        "Content-Type": "application/json",
        "X-WM-CLIENT-ID": clientId,
        "X-WM-CLIENT-SECRET": clientSecret,
        "Content-Length": Buffer.byteLength(jsonPayload)
    }
};

var request = new http.ClientRequest(options);
request.end(jsonPayload);

request.on('response', function (response) {
    console.log('Heard back from the WhatsMate Telegram Gateway:\n');
    console.log('Status code: ' + response.statusCode);
    response.setEncoding('utf8');
    response.on('data', function (chunk) {
        console.log(chunk);
    });
});