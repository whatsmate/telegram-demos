function main() {
  var group_name = "Muscle Men Club";  // TODO: Specify the name of your group
  var group_admin = "12025550108";     // TODO: Specify the number of the group admin
  var message = "Your six-pack is on the way!";
  sendTelegramGroupText(destNumber, message);
}


function sendTelegramGroupText(group_name, group_admin, message) {
  var instanceId = "YOUR_INSTANCE_ID_HERE";  // TODO: Replace it with your gateway instance ID here
  var clientId = "YOUR_CLIENT_ID_HERE";  // TODO: Replace it with your Premium Account client ID here
  var clientSecret = "YOUR_CLIENT_SECRET_HERE";   // TODO: Replace it with your Premium Account client secret here
  
  var jsonPayload = JSON.stringify({
    group_name: group_name,
    group_admin: group_admin,
    message: message
  });
  
  var options = {
    "method" : "post",
    "contentType": "application/json",
    "headers": {
      "X-WM-CLIENT-ID": clientId,
      "X-WM-CLIENT-SECRET": clientSecret
    },
    "payload" : jsonPayload,
    "Content-Length": jsonPayload.length
  };
    
  UrlFetchApp.fetch("https://api.whatsmate.net/v3/telegram/group/text/message/" + instanceId, options);
}