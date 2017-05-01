Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization


Public Class BatchTelegramMessageSender

    ''' TODO: Update the following two lines with your premium account details
    Private Const INSTANCE_ID As String = "YOUR_GATEWAY_INSTANCE_ID_HERE"
    Private Const CLIENT_ID As String = "YOUR_CLIENT_ID_HERE"
    Private Const CLIENT_SECRET As String = "YOUR_CLIENT_SECRET_HERE"

    Private Const API_URL As String = "http://api.whatsmate.net/v1/telegram/batch/message/" + INSTANCE_ID

    Public Function sendBatchMessage(ByVal numbers As String(), ByVal message As String) As Boolean
        Dim success As Boolean = True
        Dim webClient As New WebClient()

        Try
            Dim payloadObj As New BatchPayload(numbers, message)
            Dim serializer As New JavaScriptSerializer()
            Dim postData As String = serializer.Serialize(payloadObj)

            webClient.Headers("content-type") = "application/json"
            webClient.Headers("X-WM-CLIENT-ID") = CLIENT_ID
            webClient.Headers("X-WM-CLIENT-SECRET") = CLIENT_SECRET

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


    Private Class BatchPayload
        Public numbers As String()
        Public message As String

        Sub New(ByVal nums As String(), ByVal msg As String)
            numbers = nums
            message = msg
        End Sub
    End Class

End Class


Module Module1

    Sub Main()
        Dim telegramSender As New BatchTelegramMessageSender()
        ''' TODO: Update the following line with the recipients' numbers and your message
        telegramSender.sendBatchMessage({"12025550105", "12025550108"}, "Hey isn't it exciting?")
        Console.WriteLine("Press Enter to exit")
        Console.ReadLine()
    End Sub

End Module

