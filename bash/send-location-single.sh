#!/bin/bash

# TODO: If you have your own Client ID and secret, put down their values here:
INSTANCE_ID="YOUR_GATEWAY_INSTANCE_ID_HERE"
CLIENT_ID="YOUR_OWN_ID_HERE"
CLIENT_SECRET="YOUR_OWN_SECRET_HERE"

# TODO: Customize the following lines:
read -r -d '' jsonPayload << _EOM_
  {
    "number": "12025550108",
    "latitude": -33.8654383,
    "longitude": 151.1296483
  }
_EOM_

curl -X POST \
     -H "X-WM-CLIENT-ID: $CLIENT_ID" \
     -H "X-WM-CLIENT-SECRET: $CLIENT_SECRET" \
     -H "Content-Type: application/json" \
     -d "$jsonPayload"   \
     http://api.whatsmate.net/v1/telegram/single/location/$INSTANCE_ID

echo -e "\n=== END OF DEMO ==="
