#!/usr/bin/env python3

import base64
import requests


# TODO: PUt down your own Client ID and secret
instanceId = "YOUR_GATEWAY_INSTANCE_ID_HERE"
clientId = "YOUR_OWN_ID_HERE"
clientSecret = "YOUR_OWN_SECRET_HERE"


# TODO: Customize the following 5 lines
group_name = 'Muscle Men Club'  # FIXME: Put down the name of your group here
group_admin = '19159876123'     # FIXME: Put down the number of the group admin here
fullpath_to_document = "../assets/subwaymap.pdf"
fn = "anyname.pdf"
caption = "Check this out"

# Encode the document in base64 format
doc_base64 = None
with open(fullpath_to_document, 'rb') as doc:
    doc_base64 = base64.b64encode(doc.read())

headers = {
    'X-WM-CLIENT-ID': clientId, 
    'X-WM-CLIENT-SECRET': clientSecret
}

jsonBody = {
    'group_name': group_name,
    'group_admin': group_admin,
    'document': doc_base64.decode("utf-8"),
    'filename': fn,
    'caption': caption
}

r = requests.post("https://api.whatsmate.net/v3/telegram/group/document/message/%s" % instanceId, 
    headers=headers,
    json=jsonBody)

print("Status code: " + str(r.status_code))
print("RESPONSE : " + str(r.content))
