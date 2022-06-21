#!/usr/bin/env python3

import base64
import requests


# TODO: PUt down your own Client ID and secret
instanceId = "YOUR_GATEWAY_INSTANCE_ID_HERE"
clientId = "YOUR_OWN_ID_HERE"
clientSecret = "YOUR_OWN_SECRET_HERE"


# TODO: Customize the following 4 lines
number = '12025550108'  # FIXME
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
    'number': number,
    'document': doc_base64.decode("utf-8"),
    'filename': fn,
    'caption': caption
}

r = requests.post("https://api.whatsmate.net/v3/telegram/single/document/message/%s" % instanceId, 
    headers=headers,
    json=jsonBody)

print("Status code: " + str(r.status_code))
print("RESPONSE : " + str(r.content))
