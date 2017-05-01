#!/bin/bash

# TODO: Put down your own Client ID and secret here:
INSTANCE_ID="YOUR_OWN_INSTANCE_ID_HERE"
CLIENT_ID="YOUR_OWN_CLIENT_ID_HERE"
CLIENT_SECRET="YOUR_OWN_SECRET_HERE"

# TODO: Customize the following 3 files:
number="12025550108"
caption="Lovely Girl" 
base64_image=`base64 -w 0 ../assets/cute-girl.jpg`


cat > /tmp/jsonbody.txt << _EOM_
  {
    "number": "$number",
    "caption": "$caption",
    "image": "$base64_image"
  }
_EOM_


curl --show-error -X POST \
     -H "X-WM-CLIENT-ID: $CLIENT_ID" \
     -H "X-WM-CLIENT-SECRET: $CLIENT_SECRET" \
     -H "Content-Type: application/json" \
     --data-binary @/tmp/jsonbody.txt  \
     http://api.whatsmate.net/v1/telegram/single/photo/binary/$INSTANCE_ID

echo -e "\n=== END OF DEMO ==="
