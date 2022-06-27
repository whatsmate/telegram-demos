using System;
using System.Net;
using System.Web.Script.Serialization; // requires the reference 'System.Web.Extensions'
using System.IO;
using System.Text;

class TelegramOpusSender
{
    // TODO: Replace the following with your gateway instance ID, client ID and secret!
    private static string INSTANCE_ID = "YOUR_INSTANCE_ID";
    private static string CLIENT_ID = "YOUR_CLIENT_ID_HERE";
    private static string CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE";

    private static string AUDIO_SINGLE_API_URL = "https://api.whatsmate.net/v3/telegram/single/voice_note/message/" + INSTANCE_ID;

    static void Main(string[] args)
    {
        TelegramOpusSender opusSender = new TelegramOpusSender();
        // TODO: Put down your recipient's number (e.g. your own cell phone number)
        string recipient = "12025550105";
        // TODO: Remember to copy the OPUS file from ..\assets to the TEMP directory!
        string base64Content = convertFileToBase64("C:\\TEMP\\martin-luther-king.opus");
        string fn = "anyname.opus";
        string caption = "I have a dream";
        
        opusSender.sendAudio(recipient, base64Content, fn, caption);

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

    public bool sendAudio(string number, string base64Content, string fn, string caption)
    {
        bool success = true;

        try
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers["X-WM-CLIENT-ID"] = CLIENT_ID;
                client.Headers["X-WM-CLIENT-SECRET"] = CLIENT_SECRET;

                SingleAudioPayload payloadObj = new SingleAudioPayload() { number = number, voice_note = base64Content, filename = fn, caption = caption};
                string postData = (new JavaScriptSerializer()).Serialize(payloadObj);

                client.Encoding = Encoding.UTF8;
                string response = client.UploadString(AUDIO_SINGLE_API_URL, postData);
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

    public class SingleAudioPayload
    {
        public string number { get; set; }
        public string voice_note { get; set; }
        public string filename { get; set; }
        public string caption { get; set; }
    }

}
