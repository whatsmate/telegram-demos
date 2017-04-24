using System;
using System.Net;
using System.Web.Script.Serialization; // requires the reference 'System.Web.Extensions'
using System.IO;
using System.Text;

class TelegramDocumentSender
{
    // TODO: Replace the following with your gateway instance ID, client ID and secret!
    private static string INSTANCE_ID = "0";
    private static string CLIENT_ID = "YOUR_CLIENT_ID_HERE";
    private static string CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE";

    private static string DOC_BATCH_API_URL = "http://api.whatsmate.net/v1/telegram/batch/document/binary/" + INSTANCE_ID;

    static void Main(string[] args)
    {
        TelegramDocumentSender docSender = new TelegramDocumentSender();
        string[] recipients = { "12025550105" };
        string filename = "anyname.pdf";
        // TODO: Remember to copy the PDF from ..\assets to the TEMP directory!
        string base64Content = convertFileToBase64("C:\\TEMP\\subwaymap.pdf");
        
        docSender.sendDocument(recipients, filename, base64Content);

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }

    // http://stackoverflow.com/questions/25919387/c-sharp-converting-file-into-base64string-and-back-again
    static public string convertFileToBase64(string fullPathToDocument)
    {
        Byte[] bytes = File.ReadAllBytes(fullPathToDocument);
        String base64Encoded = Convert.ToBase64String(bytes);
        return base64Encoded;
    }

    public bool sendDocument(string[] numbers, string filename, string base64Content)
    {
        bool success = true;

        try
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers["X-WM-CLIENT-ID"] = CLIENT_ID;
                client.Headers["X-WM-CLIENT-SECRET"] = CLIENT_SECRET;

                Console.WriteLine(filename);

                BatchDocumentPayload payloadObj = new BatchDocumentPayload() { numbers = numbers, filename = filename, document = base64Content};
                string postData = (new JavaScriptSerializer()).Serialize(payloadObj);

                client.Encoding = Encoding.UTF8;
                string response = client.UploadString(DOC_BATCH_API_URL, postData);
                Console.WriteLine(response);
            }
        }
        catch (WebException webEx)
        {
            Console.WriteLine(((HttpWebResponse)webEx.Response).StatusCode);
            Stream stream = ((HttpWebResponse)webEx.Response).GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            String body = reader.ReadToEnd();
            Console.WriteLine(body);
            success = false;
        }

        return success;
    }

    public class BatchDocumentPayload
    {
        public string[] numbers { get; set; }
        public string filename { get; set; }
        public string document { get; set; }
    }

}
