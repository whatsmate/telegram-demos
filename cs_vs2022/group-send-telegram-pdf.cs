using System;
using System.Net;
using System.Text.Json;
using System.IO;
using System.Text;

class TelegramGroupPdfSender
{
    // TODO: Replace the following with your gateway instance ID, client ID and secret!
    private static string INSTANCE_ID = "YOUR_INSTANCE_ID";
    private static string CLIENT_ID = "YOUR_CLIENT_ID_HERE";
    private static string CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE";

    private static string DOCUMENT_GROUP_API_URL = "https://api.whatsmate.net/v3/telegram/group/document/message/" + INSTANCE_ID;

    static void Main(string[] args)
    {
        TelegramGroupPdfSender groupPdfSender = new TelegramGroupPdfSender();

        // TODO: Put down group name and group admin below
        string group_name = "Muscle Men Club";
        string group_admin = "19159876123";

        // TODO: Remember to copy the PDF from ..\assets to the TEMP directory!
        string base64Content = convertFileToBase64("C:\\TEMP\\subwaymap.pdf");
        string fn = "anyname.pdf";
        string caption = "Check this out";
        
        groupPdfSender.sendDocument(group_name, group_admin, base64Content, fn, caption);

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }

    // http://stackoverflow.com/questions/25919387/c-sharp-converting-file-into-base64string-and-back-again
    static public string convertFileToBase64(string fullPathToDoc)
    {
        Byte[] bytes = File.ReadAllBytes(fullPathToDoc);
        String base64Encoded = Convert.ToBase64String(bytes);
        return base64Encoded;
    }

    public bool sendDocument(string group_name, string group_admin, string base64Content, string fn, string caption)
    {
        bool success = true;

        try
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(DOCUMENT_GROUP_API_URL);
            httpRequest.Method = "POST";
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-WM-CLIENT-ID"] = CLIENT_ID;
            httpRequest.Headers["X-WM-CLIENT-SECRET"] = CLIENT_SECRET;

            GroupDocPayload payloadObj = new GroupDocPayload() { group_name = group_name, group_admin = group_admin, document = base64Content, filename = fn, caption = caption};
            string postData = JsonSerializer.Serialize(payloadObj);

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(postData);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
        }
        catch (WebException webExcp)
        {
            Console.WriteLine("A WebException has been caught.");
            Console.WriteLine(webExcp.ToString());
            WebExceptionStatus status = webExcp.Status;
            if (status == WebExceptionStatus.ProtocolError)
            {
                Console.Write("The REST API server returned a protocol error: ");
                HttpWebResponse? httpResponse = webExcp.Response as HttpWebResponse;
                Stream stream = httpResponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                String body = reader.ReadToEnd();
                Console.WriteLine((int)httpResponse.StatusCode + " - " + body);
                success = false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("A general exception has been caught.");
            Console.WriteLine(e.ToString());
            success = false;
        }

        return success;
    }

    public class GroupDocPayload
    {
        public string group_name { get; set; }
        public string group_admin { get; set; }
        public string document { get; set; }
        public string filename { get; set; }
        public string caption { get; set; }
    }

}
