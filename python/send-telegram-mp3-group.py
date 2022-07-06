#!/usr/bin/env python3

import base64
import requests


# TODO: Put down your own Client ID and secret
instanceId = "YOUR_GATEWAY_INSTANCE_ID_HERE"
clientId = "YOUR_OWN_ID_HERE"
clientSecret = "YOUR_OWN_SECRET_HERE"


# TODO: Customize the following 5 lines
group_name = 'Muscle Men Club'  # FIXME: Put down the name of your group here
group_admin = '19159876123'     # FIXME: Put down the number of the group admin here
fullpath_to_mp3 = "../assets/ocean-waves.mp3"
fn = "anyname.mp3"
caption = "Enjoy the nature"

# Encode the MP3 file in base64 format
audio_base64 = None
with open(fullpath_to_mp3, 'rb') as binary_content:
    audio_base64 = base64.b64encode(binary_content.read())

headers = {
    'X-WM-CLIENT-ID': clientId, 
    'X-WM-CLIENT-SECRET': clientSecret
}

jsonBody = {
    'group_name': group_name,
    'group_admin': group_admin,
    'audio': audio_base64.decode("utf-8"),
    'filename': fn,
    'caption': caption
}

r = requests.post("https://api.whatsmate.net/v3/telegram/group/audio/message/%s" % instanceId, 
    headers=headers,
    json=jsonBody)

print("Status code: " + str(r.status_code))
print("RESPONSE : " + str(r.content))
