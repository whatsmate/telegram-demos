#!/bin/bash

# TODO: If you have your own Client ID and secret, put down their values here:
INSTANCE_ID="YOUR_OWN_INSTANCE_ID_HERE"
CLIENT_ID="YOUR_CLIENT_ID_HERE"
CLIENT_SECRET="YOUR_CLIENT_SECRET_HERE"


read -r -d '' jsonPayload << _EOM_
  {
    "number": "12025550108",
    "message": "Loving you! Have a nice day :)"
  }
_EOM_

curl -X POST \
     -H "X-WM-CLIENT-ID: $CLIENT_ID" \
     -H "X-WM-CLIENT-SECRET: $CLIENT_SECRET" \
     -H "Content-Type: application/json" \
     -d "$jsonPayload"   \
     http://api.whatsmate.net/v1/telegram/single/message/$INSTANCE_ID

echo -e "\n=== END OF DEMO ==="
