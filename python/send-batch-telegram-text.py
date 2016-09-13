#!/usr/bin/env python

import requests

# TODO: You will need your own Client ID and secret to call this API. Subscribe now if haven't already.
clientId = "YOUR_OWN_ID_HERE"
clientSecret = "YOUR_OWN_SECRET_HERE"

jsonBody = {
    'numbers': ['12025550108', '12025550109'],  # FIXME: Put down your registered numbers here
    'message': 'Have a nice day! Loving you:)'  # FIXME: Customize your own message  
}

headers = {
    'X-WM-CLIENT-ID': clientId, 
    'X-WM-CLIENT-SECRET': clientSecret
}

r = requests.post('http://api.whatsmate.net/v1/telegram/batch/message/0',
    headers=headers,
    json=jsonBody)

print("Status code: " + str(r.status_code))
print("Response: " + str(r.content))

