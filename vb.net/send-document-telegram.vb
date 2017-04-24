Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web.Script.Serialization


Public Class TelegramDocumentSender

    ''' TODO: Update the following with your gateway instance ID, client ID and secret.
    Private Const INSTANCE_ID As String = "0"
    Private Const CLIENT_ID As String = "YOUR_CLIENT_ID_HERE"
    Private Const CLIENT_SECRET As String = "YOUR_CLIENT_SECRET_HERE"

    Private Const BATCH_DOC_API_URL As String = "http://api.whatsmate.net/v1/telegram/batch/document/binary/" + INSTANCE_ID

    Public Function sendDocumentFile(ByVal numbers As String(), ByVal filename As String, ByVal base64Content As String) As Boolean
        Dim success As Boolean = True
        Dim webClient As New WebClient()

        Try
            Dim payloadObj As New BatchDocumentPayload(numbers, filename, base64Content)
            Dim serializer As New JavaScriptSerializer()
            Dim postData As String = serializer.Serialize(payloadObj)

            webClient.Encoding = Encoding.UTF8
            webClient.Headers("content-type") = "application/json"
            webClient.Headers("X-WM-CLIENT-ID") = CLIENT_ID
            webClient.Headers("X-WM-CLIENT-SECRET") = CLIENT_SECRET

            Dim response As String = webClient.UploadString(BATCH_DOC_API_URL, postData)
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


    Private Class BatchDocumentPayload
        Public numbers As String()
        Public filename As String
        Public document As String

        Sub New(ByVal nums As String(), ByVal fn As String, ByVal base64Content As String)
            numbers = nums
            filename = fn
            document = base64Content
        End Sub
    End Class

End Class


Module Module1
    Sub Main()
        Dim telegramDocSender As New TelegramDocumentSender()a
        ' TODO: Remember to copy the PDF from ..\assets to the TEMP directory!
        Dim base64Content As String = ConvertFileToBase64("C:\TEMP\subwaymap.pdf")

        telegramDocSender.sendDocumentFile({"12025550105"}, "subway.pdf", base64Content)
        Console.WriteLine("Press Enter to exit")
        Console.ReadLine()
    End Sub

    '''
    ''' Credits: http://stackoverflow.com/questions/10739264/convert-file-to-base64-function-output
    '''  
    Public Function ConvertFileToBase64(ByVal fullPathToDocument As String) As String
        Return Convert.ToBase64String(System.IO.File.ReadAllBytes(fullPathToDocument))
    End Function
End Module


