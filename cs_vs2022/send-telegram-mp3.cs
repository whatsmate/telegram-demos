using System;
using System.Net;
using System.Text.Json;
using System.IO;
using System.Text;

class TelegramMp3Sender
{
    // TODO: Replace the following with your gateway instance ID, client ID and secret!
    private static string INSTANCE_ID = "YOUR_INSTANCE_ID";
    private static string CLIENT_ID = "YOUR_CLIENT_ID_HERE";
    private static string CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE";

    private static string AUDIO_SINGLE_API_URL = "https://api.whatsmate.net/v3/telegram/single/audio/message/" + INSTANCE_ID;

    static void Main(string[] args)
    {
        TelegramMp3Sender mp3Sender = new TelegramMp3Sender();
        // TODO: Put down your recipient's number (e.g. your own cell phone number)
        string recipient = "12025550105";
        // TODO: Remember to copy the MP3 file from ..\assets to the TEMP directory!
        string base64Content = convertFileToBase64("C:\\TEMP\\ocean-waves.mp3");
        string fn = "anyname.mp3";
        string caption = "Enjoy the nature";
        
        mp3Sender.sendAudio(recipient, base64Content, fn, caption);

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
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(AUDIO_SINGLE_API_URL);
            httpRequest.Method = "POST";
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-WM-CLIENT-ID"] = CLIENT_ID;
            httpRequest.Headers["X-WM-CLIENT-SECRET"] = CLIENT_SECRET;

            SingleAudioPayload payloadObj = new SingleAudioPayload() { number = number, audio = base64Content, filename = fn, caption = caption};
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

    public class SingleAudioPayload
    {
        public string number { get; set; }
        public string audio { get; set; }
        public string filename { get; set; }
        public string caption { get; set; }
    }

}
