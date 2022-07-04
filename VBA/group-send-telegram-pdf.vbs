Sub Main_Routine()
  ''' The first parameter is the name of the group
  ''' The second parameter is the number of the group admin 
  ''' The third parameter is the full path to the document file (e.g. PDF)
  ''' The fourth parameter is the filename displayed to the recipient.
  ''' The fifth parameter is the caption for the document.
  TelegramDoc_GroupSend "Muscle Men Club", "19159876123", "C:\Users\Public\subwaymap.pdf", "click_me.pdf", "Check this out"     ''' TODO: Specify the group name and group admin
End Sub


Sub TelegramDoc_GroupSend(ByRef group_name As String, ByRef group_admin As String, ByRef strFilename As String, ByRef strDisplayName As String, ByRef strCaption As String)
  Dim INSTANCE_ID As String, CLIENT_ID As String, CLIENT_SECRET As String, API_URL As String
  Dim strJson As Variant
  Dim sHTML As String
  Dim oHttp As Object
  Dim contentInBase64 As String

  ''' TODO: Replace the following with your gateway instance ID, your Premium Account client ID and secret:
  INSTANCE_ID = "YOUR_INSTANCE_ID_HERE"
  CLIENT_ID = "YOUR_CLIENT_ID_HERE"
  CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE"
  API_URL = "https://api.whatsmate.net/v3/telegram/group/document/message/" & INSTANCE_ID

  contentInBase64 = ConvertFileToBase64(strFilename)
  strJson = "{""group_name"": """ & group_name &  """, ""group_admin"": """ & group_admin &  """, ""document"": """ & contentInBase64 & """, ""filename"": """ & strDisplayName & """, ""caption"": """ & strCaption & """}"

  Set oHttp = CreateObject("Msxml2.XMLHTTP")
  oHttp.Open "POST", API_URL, False
  oHttp.setRequestHeader "Content-type", "application/json"
  oHttp.setRequestHeader "X-WM-CLIENT-ID", CLIENT_ID
  oHttp.setRequestHeader "X-WM-CLIENT-SECRET", CLIENT_SECRET
  oHttp.Send strJson

  sHTML = oHttp.ResponseText
  MsgBox sHTML
End Sub


''' =================================================================
''' You do not need to change anything below this line.
''' =================================================================
''' The following function is taken from the page below. Author: Cain Hill
''' https://medium.com/cainhill/how-to-use-vba-to-convert-a-file-to-base-64-d124c9b2958a

Public Function ConvertFileToBase64(strFilePath As String) As String

    Const UseBinaryStreamType = 1

    Dim streamInput: Set streamInput = CreateObject("ADODB.Stream")
    Dim xmlDoc: Set xmlDoc = CreateObject("Microsoft.XMLDOM")
    Dim xmlElem: Set xmlElem = xmlDoc.createElement("tmp")

    streamInput.Open
    streamInput.Type = UseBinaryStreamType
    streamInput.LoadFromFile strFilePath
    xmlElem.DataType = "bin.base64"
    xmlElem.nodeTypedValue = streamInput.Read
    ConvertFileToBase64 = Replace(xmlElem.text, vbLf, "")

    Set streamInput = Nothing
    Set xmlDoc = Nothing
    Set xmlElem = Nothing

End Function
