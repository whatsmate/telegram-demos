#!/bin/bash

# TODO: Put down your own Client ID and secret here:
CLIENT_ID="YOUR_OWN_CLIENT_ID_HERE"
CLIENT_SECRET="YOUR_OWN_SECRET_HERE"

# TODO: Customize the following 3 files:
number="121838828274"
filename="ocean-waves.mp3"
base64_audio=`base64 -w 0 ../assets/ocean-waves.mp3`

cat > /tmp/jsonbody.txt << _EOM_
  {
    "number": "$number",
    "filename": "$filename",
    "audio": "$base64_audio"
  }
_EOM_


curl --show-error -X POST \
     -H "X-WM-CLIENT-ID: $CLIENT_ID" \
     -H "X-WM-CLIENT-SECRET: $CLIENT_SECRET" \
     -H "Content-Type: application/json" \
     --data-binary @/tmp/jsonbody.txt  \
     http://api.whatsmate.net/v1/telegram/single/audio/binary/0

echo -e "\n=== END OF DEMO ==="
