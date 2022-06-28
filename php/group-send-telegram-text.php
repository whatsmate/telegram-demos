<?php
$INSTANCE_ID = 'YOUR_INSTANCE_ID_HERE';  // TODO: Replace it with your gateway instance ID here
$CLIENT_ID = "YOUR_CLIENT_ID_HERE";  // TODO: Replace it with your Premium Account client ID here
$CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE";   // TODO: Replace it with your Premium Account client secret here

$postData = array(
  'group_name' => 'Muscle Men Club',  // TODO: Specify the name of the group
  'group_admin' => '12025550108',     // TODO: Specify the number of the group admin
  'message' => 'Your six-pack is on the way!'
);

$headers = array(
  'Content-Type: application/json',
  'X-WM-CLIENT-ID: '.$CLIENT_ID,
  'X-WM-CLIENT-SECRET: '.$CLIENT_SECRET
);

$url = 'https://api.whatsmate.net/v3/telegram/group/text/message/' . $INSTANCE_ID;
$ch = curl_init($url);

curl_setopt($ch, CURLOPT_POST, 1);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
curl_setopt($ch, CURLOPT_POSTFIELDS, json_encode($postData));

$response = curl_exec($ch);

echo "Response: ".$response;
curl_close($ch);
?>