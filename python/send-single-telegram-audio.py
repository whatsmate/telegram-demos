#!/usr/bin/env python

import base64
import requests


# TODO: When you have your own Client ID and secret, put down their values here:
instanceId = "YOUR_GATEWAY_INSTANCE_ID_HERE"
clientId = "YOUR_OWN_ID_HERE"
clientSecret = "YOUR_OWN_SECRET_HERE"

number = '12025550108'  # FIXME
filename = "ocean.mp3"
fullpath_to_photo = "../assets/ocean-waves.mp3"

# Encode photo in base64 format
image_base64 = None
with open(fullpath_to_photo, 'rb') as audio:
    audio_base64 = base64.b64encode(audio.read())

headers = {
    'X-WM-CLIENT-ID': clientId, 
    'X-WM-CLIENT-SECRET': clientSecret
}

jsonBody = {
    'number': number,
    'filename': filename,
    'audio': audio_base64
}

r = requests.post("http://api.whatsmate.net/v1/telegram/single/audio/binary/%s" % instanceId,
    headers=headers,
    json=jsonBody)

print("Status code: " + str(r.status_code))
print("RESPONSE : " + str(r.content))
