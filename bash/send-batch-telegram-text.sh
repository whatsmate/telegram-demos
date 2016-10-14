#!/bin/bash

# TODO: If you have your own Client ID and secret, put down their values here:
CLIENT_ID="YOUR_OWN_ID_HERE"
CLIENT_SECRET="YOUR_OWN_SECRET_HERE"

read -r -d '' jsonPayload << _EOM_
  {
    "numbers": ["12025550108", "12025550109"],
    "message": "Loving you! Have a nice day :)"
  }
_EOM_

curl -X POST \
     -H "X-WM-CLIENT-ID: $CLIENT_ID" \
     -H "X-WM-CLIENT-SECRET: $CLIENT_SECRET" \
     -H "Content-Type: application/json" \
     -d "$jsonPayload"   \
     http://api.whatsmate.net/v1/telegram/batch/message/0

echo -e "\n=== END OF DEMO ==="
