Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web.Script.Serialization


Public Class TelegramMessageSender

    ''' TODO: Replace the following with your gateway instance ID, Premium Account client ID and secret below:
    Private Const INSTANCE_ID As String = "YOUR_INSTANCE_ID_HERE"
    Private Const CLIENT_ID As String = "YOUR_CLIENT_ID_HERE"
    Private Const CLIENT_SECRET As String = "YOUR_CLIENT_SECRET_HERE"

    Private Const API_URL As String = "https://api.whatsmate.net/v3/telegram/single/text/message/" + INSTANCE_ID

    Public Function sendMessage(ByVal number As String, ByVal message As String) As Boolean
        Dim success As Boolean = True
        Dim webClient As New WebClient()

        Try
            Dim payloadObj As New Payload(number, message)
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


    Private Class Payload
        Public number As String
        Public message As String

        Sub New(ByVal num As String, ByVal msg As String)
            number = num
            message = msg
        End Sub
    End Class

End Class


Module Module1

    Sub Main()
        Dim waSender As New TelegramMessageSender()
        waSender.sendMessage("12025550108", "Hey isn't it exciting?")  ''' TODO: Specify the recipient's number here. NOT the gateway number
        Console.WriteLine("Press Enter to exit")
        Console.ReadLine()
    End Sub

End Module