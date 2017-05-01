#!/usr/bin/env python

import requests

# TODO: When you have your own Client ID and secret, put down their values here:
instanceId = "YOUR_GATEWAY_INSTANCE_ID_HERE"
clientId = "YOUR_OWN_ID_HERE"
clientSecret = "YOUR_OWN_SECRET_HERE"

jsonBody = {
    'numbers': ['12025550108', '12025550109'],  # FIXME: Put down your registered number here
    'latitude': -33.8654383,  # FIXME: Specify your latitude here
    'longitude': 151.1296483  # FIXME: Specify your longitude here
}

headers = {
    'X-WM-CLIENT-ID': clientId, 
    'X-WM-CLIENT-SECRET': clientSecret
}

r = requests.post("http://api.whatsmate.net/v1/telegram/batch/location/%s" % instanceId,
    headers=headers,
    json=jsonBody)

print("Status code: " + str(r.status_code))
print("Response: " + str(r.content))

