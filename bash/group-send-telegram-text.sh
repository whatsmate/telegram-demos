#!/bin/bash

INSTANCE_ID="YOUR_INSTANCE_ID_HERE"  # TODO: Replace it with your gateway instance ID here
CLIENT_ID="YOUR_CLIENT_ID_HERE"  # TODO: Replace it with your "Premium Account" client ID here
CLIENT_SECRET="YOUR_CLIENT_SECRET_HERE"   # TODO: Replace it with your "Premium Account" client secret here

# TODO: Specify the group name and the admin's number on lines 10 and 11.
read -r -d '' jsonPayload << _EOM_
  {
    "group_name": "Muscle Men Club",
    "group_admin": "19159876123",
    "message": "Your six-pack is on the way!"
  }
_EOM_

curl -X POST \
     -H "X-WM-CLIENT-ID: $CLIENT_ID" \
     -H "X-WM-CLIENT-SECRET: $CLIENT_SECRET" \
     -H "Content-Type: application/json" \
     -d "$jsonPayload"   \
     https://api.whatsmate.net/v3/telegram/group/text/message/$INSTANCE_ID