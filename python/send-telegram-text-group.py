#!/usr/bin/env python3

import requests

# TODO: When you have your own Client ID and secret, put down their values here:
instanceId = "YOUR_GATEWAY_INSTANCE_ID_HERE"
clientId = "YOUR_OWN_ID_HERE"
clientSecret = "YOUR_OWN_SECRET_HERE"


# TODO: Customize the following 2 lines
group_name = 'Muscle Men Club'  # FIXME: Put down the name of your group here
group_admin = '19159876123'     # FIXME: Put down the number of the group admin here
message = "Your six-pack is on the way!"  # FIXME: Put down your own text message here.

headers = {
    'X-WM-CLIENT-ID': clientId, 
    'X-WM-CLIENT-SECRET': clientSecret
}

jsonBody = {
    'group_name': group_name,
    'group_admin': group_admin,
    'message': message
}

r = requests.post("https://api.whatsmate.net/v3/telegram/group/text/message/%s" % instanceId, 
    headers=headers,
    json=jsonBody)

print("Status code: " + str(r.status_code))
print("RESPONSE : " + str(r.content))