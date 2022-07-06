#!/bin/bash

#######################################################################
# You will need to have the utility `base64` available on your Linux
# system to run this script.
#
# To install it:
# sudo apt-get install coreutils
#######################################################################


# TODO: Put down your own Client ID and secret here:
INSTANCE_ID="YOUR_OWN_INSTANCE_ID_HERE"
CLIENT_ID="YOUR_OWN_CLIENT_ID_HERE"
CLIENT_SECRET="YOUR_OWN_SECRET_HERE"

# TODO: Customize the following 3 files:
group_name="Muscle Men Club"
group_admin="12025550108"
base64_audio=`base64 -w 0 ../assets/ocean-waves.mp3`
fn="ocean.mp3"
caption="Enjoy the nature"

cat > /tmp/jsonbody.txt << _EOM_
  {
    "group_name": "$group_name",
    "group_admin": "$group_admin",
    "audio": "$base64_audio",
    "filename": "$fn",
    "caption": "$caption"
  }
_EOM_

curl --show-error -X POST \
     -H "X-WM-CLIENT-ID: $CLIENT_ID" \
     -H "X-WM-CLIENT-SECRET: $CLIENT_SECRET" \
     -H "Content-Type: application/json" \
     --data-binary @/tmp/jsonbody.txt  \
     https://api.whatsmate.net/v3/telegram/group/audio/message/$INSTANCE_ID

echo -e "\n=== END OF DEMO ==="
