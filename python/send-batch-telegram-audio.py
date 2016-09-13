#!/usr/bin/env python

import base64
import requests


# TODO: When you have your own Client ID and secret, put down their values here:
clientId = "YOUR_OWN_ID_HERE"
clientSecret = "YOUR_OWN_SECRET_HERE"


numbers = ['12025550108', '12025550109']  # FIXME
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
    'numbers': numbers,
    'filename': filename,
    'audio': audio_base64
}

r = requests.post('http://api.whatsmate.net/v1/telegram/batch/audio/binary/0',
    headers=headers,
    json=jsonBody)

print("Status code: " + str(r.status_code))
print("RESPONSE : " + str(r.content))
