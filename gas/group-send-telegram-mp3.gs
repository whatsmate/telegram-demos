function demoSendMp3ToTelegramGroup() {
  var group_name = "Muscle Men Club";  // TODO: Specify the name of your group
  var group_admin = "12025550108";     // TODO: Specify the number of the group admin
  var mp3Filename = "ocean-waves.mp3";  // TODO: This file must be uniquely named and, of course, exist in your Google Drive!!!
  var filename = "anyname.mp3";    // TODO: Use any name you like
  var caption = "Enjoy the nature";  // TODO: Use any caption you like

  var mp3Base64 = base64encodeFileByName(mp3Filename);
  if (mp3Base64 == null) {
    Logger.log("Abort! MP3 file error: " + mp3Filename);
    return;
  }
  
  sendMp3ToTelegramGroup(group_name, group_admin, mp3Base64, filename, caption);
}


function sendMp3ToTelegramGroup(group_name, group_admin, mp3Base64, filename, caption) {
  var instanceId = "YOUR_INSTANCE_ID_HERE";  // TODO: Replace it with your gateway instance ID here
  var clientId = "YOUR_CLIENT_ID_HERE";  // TODO: Replace it with your Premium Account client ID here
  var clientSecret = "YOUR_CLIENT_SECRET_HERE";   // TODO: Replace it with your Premium Account client secret here
  
  var jsonPayload = JSON.stringify({
    group_name: group_name,
    group_admin: group_admin,
    audio: mp3Base64,
    filename: filename,
    caption: caption
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
  
  Logger.log("Calling API to send MP3 to this group  " + group_name);
  UrlFetchApp.fetch("https://api.whatsmate.net/v3/telegram/group/audio/message/" + instanceId, options);
}


function base64encodeFileByName(filename) {
  var file = retrieveFileByName(filename);
  if (file == null) {
    Logger.log("File cannot be found. Aborting...");
    return null;
  }
  
  var base64Content = encodeBlobAsBase64(file.getBlob());
  return base64Content;
}


function encodeBlobAsBase64(blob) {
  var base64String = Utilities.base64Encode(blob.getBytes());
  return base64String;
}


function retrieveFileById(fileId) {
  var targetFile = null;
  
  try {
    targetFile = DriveApp.getFileById(fileId);
  }
  catch(err) {
    Logger.log("File not found. ID: " + fileId); 
  }
  
  return targetFile;
}


function retrieveFileByName(filename) {  
  var matchedFiles = DriveApp.getFilesByName(filename);
  
  // assume the first match is the target
  var targetFile = null;
  if (matchedFiles.hasNext()) {
    targetFile = matchedFiles.next();
    Logger.log("Target file found. Name is  " + targetFile.getName());
    Logger.log("Target file ID is " + targetFile.getId());
  } else {
    Logger.log("File not found: " + filename);
  }
  
  return targetFile;
}

