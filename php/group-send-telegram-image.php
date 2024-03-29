<?php
  $INSTANCE_ID = 'YOUR_INSTANCE_ID_HERE';  // TODO: Replace it with your gateway instance ID here
  $CLIENT_ID = "YOUR_CLIENT_ID_HERE";  // TODO: Replace it with your Premium client ID here
  $CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE";   // TODO: Replace it with your Premium client secret here

  $pathToImage = "/tmp/your_image.jpg";    // TODO: Replace it with the path to your image
  $imageData = file_get_contents($pathToImage);
  $base64Image = base64_encode($imageData);
  $caption = "Lovely Girl";

  $postData = array(
    'group_name' => 'Muscle Men Club',  // TODO: Specify the name of the group
    'group_admin' => '12025550108',     // TODO: Specify the number of the group admin
    'image' => $base64Image,
    'caption' => $caption
  );

  $headers = array(
    'Content-Type: application/json',
    'X-WM-CLIENT-ID: '.$CLIENT_ID,
    'X-WM-CLIENT-SECRET: '.$CLIENT_SECRET
  );

  $url = 'https://api.whatsmate.net/v3/telegram/group/image/message/' . $INSTANCE_ID;
  $ch = curl_init($url);
  curl_setopt($ch, CURLOPT_POST, 1);
  curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
  curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
  curl_setopt($ch, CURLOPT_POSTFIELDS, json_encode($postData));
  $response = curl_exec($ch);
  echo "Response: ".$response;
  curl_close($ch);
?>