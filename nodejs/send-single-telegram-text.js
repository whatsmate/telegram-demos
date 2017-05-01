#!/usr/bin/env node

var http = require('http');

// When you have your own Client ID and secret, put down their values here:
var instanceId = "YOUR_GATEWAY_INSTANCE_ID";
var clientId = "YOUR_CLIENT_ID_HERE";
var clientSecret = "YOUR_CLIENT_SECRET_HERE";

var jsonPayload = JSON.stringify({
    number: "12025550108",  // FIXME
    message: "Have a nice day! Loving you:)"  // FIXME
});

var options = {
    hostname: "api.whatsmate.net",
    port: 80,
    path: "/v1/telegram/single/message/" + instanceId,
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
    console.log('Heard back from WhatsMate Telegram Gateway:\n');
    console.log('Status code: ' + response.statusCode);
    response.setEncoding('utf8');
    response.on('data', function (chunk) {
        console.log(chunk);
    });
});
