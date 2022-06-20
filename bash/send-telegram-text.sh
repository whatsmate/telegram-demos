#!/bin/bash

INSTANCE_ID="YOUR_INSTANCE_ID_HERE"  # TODO: Replace it with your gateway instance ID here
CLIENT_ID="YOUR_CLIENT_ID_HERE"  # TODO: Replace it with your "Premium Account" client ID here
CLIENT_SECRET="YOUR_CLIENT_SECRET_HERE"   # TODO: Replace it with your "Premium Account" client secret here

# TODO: Specify the recipient's number (NOT the gateway number) on line 10.
read -r -d '' jsonPayload << _EOM_
  {
    "number": "12025550108",
    "message": "Howdy! Is this exciting?"
  }
_EOM_

curl -X POST \
     -H "X-WM-CLIENT-ID: $CLIENT_ID" \
     -H "X-WM-CLIENT-SECRET: $CLIENT_SECRET" \
     -H "Content-Type: application/json" \
     -d "$jsonPayload"   \
     https://api.whatsmate.net/v3/telegram/single/text/message/$INSTANCE_ID