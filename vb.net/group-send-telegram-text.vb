Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web.Script.Serialization


Public Class TelegramGroupMessageSender

    ''' TODO: Replace the following with your gateway instance ID, Premium Account client ID and secret below:
    Private Const INSTANCE_ID As String = "YOUR_INSTANCE_ID_HERE"
    Private Const CLIENT_ID As String = "YOUR_CLIENT_ID_HERE"
    Private Const CLIENT_SECRET As String = "YOUR_CLIENT_SECRET_HERE"

    Private Const API_URL As String = "https://api.whatsmate.net/v3/telegram/group/text/message/" + INSTANCE_ID

    Public Function sendGroupMessage(ByVal number As String, ByVal message As String) As Boolean
        Dim success As Boolean = True
        Dim webClient As New WebClient()

        Try
            Dim payloadObj As New GroupPayload(number, message)
            Dim serializer As New JavaScriptSerializer()
            Dim postData As String = serializer.Serialize(payloadObj)

            webClient.Headers("content-type") = "application/json"
            webClient.Headers("X-WM-CLIENT-ID") = CLIENT_ID
            webClient.Headers("X-WM-CLIENT-SECRET") = CLIENT_SECRET

            webClient.Encoding = Encoding.UTF8
            Dim response As String = webClient.UploadString(API_URL, postData)
            Console.WriteLine(response)
        Catch webEx As WebException
            Dim res As HttpWebResponse = DirectCast(webEx.Response, HttpWebResponse)
            Dim stream As Stream = res.GetResponseStream()
            Dim reader As New StreamReader(stream)
            Dim body As String = reader.ReadToEnd()

            Console.WriteLine(res.StatusCode)
            Console.WriteLine(body)
            success = False
        End Try

        Return success
    End Function


    Private Class GroupPayload
        Public group_name As String
        Public group_admin As String
        Public message As String

        Sub New(ByVal group_name As String, ByVal group_admin As String, ByVal msg As String)
            group_name = group_name
            group_admin = group_admin
            message = msg
        End Sub
    End Class

End Class


Module Module1

    Sub Main()
        Dim telegramGroupSender As New TelegramGroupMessageSender()
        telegramGroupSender.sendGroupMessage("Muscle Men Club", "19159876123", "Your six-pack is on the way!")  ''' TODO: Specify the group name and group admin
        Console.WriteLine("Press Enter to exit")
        Console.ReadLine()
    End Sub

End Module