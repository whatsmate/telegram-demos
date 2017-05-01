#!/usr/bin/env python

import base64
import requests


# TODO: When you have your own Client ID and secret, put down their values here:
instanceId = "YOUR_GATEWAY_INSTANCE_ID_HERE"
clientId = "YOUR_OWN_ID_HERE"
clientSecret = "YOUR_OWN_SECRET_HERE"

numbers = ['12025550108', '12025550109']  # FIXME
filename = "ny-subway.pdf"
fullpath_to_pdf = "../assets/subwaymap.pdf"

# Encode photo in base64 format
image_base64 = None
with open(fullpath_to_pdf, 'rb') as content:
    base64content = base64.b64encode(content.read())

headers = {
    'X-WM-CLIENT-ID': clientId, 
    'X-WM-CLIENT-SECRET': clientSecret
}

jsonBody = {
    'numbers': numbers,
    'filename': filename,
    'document': base64content
}

r = requests.post("https://api.whatsmate.net/v1/telegram/batch/document/binary/%s" % instanceId,
    headers=headers,
    json=jsonBody)

print("Status code: " + str(r.status_code))
print("RESPONSE : " + str(r.content))
