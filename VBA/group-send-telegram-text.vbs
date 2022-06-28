Sub Main_Routine()
  ''' The first parameter is the name of the group
  ''' The second parameter is the number of the group admin 
  ''' The third paramter is the content of the message.
  TelegramMessage_SendToGroup "Muscle Men Club", "19159876123", "Your six-pack is on the way!"   ''' TODO: Specify the group name and group admin
End Sub


Sub TelegramMessage_SendToGroup(ByRef group_name As String, ByRef group_admin As String, ByRef strMessage As String)
  Dim INSTANCE_ID As String, CLIENT_ID As String, CLIENT_SECRET As String, API_URL As String
  Dim strJson As Variant
  Dim sHTML As String
  Dim oHttp As Object
  
  ''' TODO: Replace the following with your gateway instance ID, your Premium Account client ID and secret:
  INSTANCE_ID = "YOUR_INSTANCE_ID_HERE"
  CLIENT_ID = "YOUR_CLIENT_ID_HERE"
  CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE"
  API_URL = "https://api.whatsmate.net/v3/telegram/group/text/message/" & INSTANCE_ID

  strJson = "{""group_name"": """ & group_name &  """, ""group_admin"": """ & group_admin &  """, ""message"": """ & strMessage & """}"

  Set oHttp = CreateObject("Msxml2.XMLHTTP")
  oHttp.Open "POST", API_URL, False
  oHttp.setRequestHeader "Content-type", "application/json"
  oHttp.setRequestHeader "X-WM-CLIENT-ID", CLIENT_ID
  oHttp.setRequestHeader "X-WM-CLIENT-SECRET", CLIENT_SECRET
  oHttp.Send strJson

  sHTML = oHttp.ResponseText
  MsgBox sHTML
End Sub