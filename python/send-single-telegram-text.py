#!/usr/bin/env python

import requests

# TODO: When you have your own Client ID and secret, put down their values here:
clientId = "YOUR_CLIENT_ID_HERE"
clientSecret = "YOUR_CLIENT_SECRET_HERE"

jsonBody = {
    'number': '12025550108',  # FIXME: Put down your registered number here
    'message': 'Have a nice day! Loving you:)'  # FIXME: Customize your own message  
}

headers = {
    'X-WM-CLIENT-ID': clientId, 
    'X-WM-CLIENT-SECRET': clientSecret
}

r = requests.post('http://api.whatsmate.net/v1/telegram/single/message/0',
    headers=headers,
    json=jsonBody)

print("Status code: " + str(r.status_code))
print("Response: " + str(r.content))

