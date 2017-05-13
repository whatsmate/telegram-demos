Sub Main_Routine()
    WhatsAppMessage_Send "12025550108", "God Loves You"
End Sub


Sub WhatsAppMessage_Send(ByRef strNumber As String, ByRef strMessage As String)
    Dim CLIENT_ID As String, CLIENT_SECRET As String, API_URL As String
    Dim strJson As Variant
    Dim sHTML As String
    Dim oHttp As Object
    
    INSTANCE_ID = "YOUR_GATEWAY_INSTANCE_ID_HERE"
    CLIENT_ID = "YOUR_CLIENT_ID_HERE"
    CLIENT_SECRET = "YOUR_SECRET_HERE"
    API_URL = "https://api.whatsmate.net/v2/whatsapp/single/message/" & INSTANCE_ID
   
    strJson = "{""number"": """ & strNumber & """, ""message"": """ & strMessage & """}"
    
    Set oHttp = CreateObject("Msxml2.XMLHTTP")
    oHttp.Open "POST", API_URL, False
    oHttp.setRequestHeader "Content-type", "application/json"
    oHttp.setRequestHeader "X-WM-CLIENT-ID", CLIENT_ID
    oHttp.setRequestHeader "X-WM-CLIENT-SECRET", CLIENT_SECRET
    oHttp.Send strJson

    sHTML = oHttp.ResponseText
    MsgBox sHTML
End Sub

