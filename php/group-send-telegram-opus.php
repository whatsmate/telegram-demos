<?php
  $INSTANCE_ID = 'YOUR_INSTANCE_ID_HERE';  // TODO: Replace it with your gateway instance ID here
  $CLIENT_ID = "YOUR_CLIENT_ID_HERE";  // TODO: Replace it with your Premium client ID here
  $CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE";   // TODO: Replace it with your Premium client secret here

  $pathToDocument = "/tmp/martin-luther-king.opus";    // TODO: Replace it with the path to your voice note file
  $voiceData = file_get_contents($pathToDocument);
  $base64VoiceNote = base64_encode($voiceData);
  $fn = "anyname.opus";                      // TODO: Replace it with any name you like
  $caption = "I have a dream";              // TODO: Replace it with an eye-catching caption

  $postData = array(
    'group_name' => 'Muscle Men Club',  // TODO: Specify the name of the group
    'group_admin' => '12025550108',     // TODO: Specify the number of the group admin
    'voice_note' => $base64VoiceNote,
    'filename' => $fn,
    'caption' => $caption
  );

  $headers = array(
    'Content-Type: application/json',
    'X-WM-CLIENT-ID: '.$CLIENT_ID,
    'X-WM-CLIENT-SECRET: '.$CLIENT_SECRET
  );

  $url = 'https://api.whatsmate.net/v3/telegram/group/voice_note/message/' . $INSTANCE_ID;
  $ch = curl_init($url);
  curl_setopt($ch, CURLOPT_POST, 1);
  curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
  curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
  curl_setopt($ch, CURLOPT_POSTFIELDS, json_encode($postData));
  $response = curl_exec($ch);
  echo "Response: ".$response;
  curl_close($ch);
?>
