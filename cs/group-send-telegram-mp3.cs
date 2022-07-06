using System;
using System.Net;
using System.Web.Script.Serialization; // requires the reference 'System.Web.Extensions'
using System.IO;
using System.Text;

class TelegramGroupMp3Sender
{
    // TODO: Replace the following with your gateway instance ID, client ID and secret!
    private static string INSTANCE_ID = "YOUR_INSTANCE_ID";
    private static string CLIENT_ID = "YOUR_CLIENT_ID_HERE";
    private static string CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE";

    private static string AUDIO_GROUP_API_URL = "https://api.whatsmate.net/v3/telegram/group/audio/message/" + INSTANCE_ID;

    static void Main(string[] args)
    {
        TelegramGroupMp3Sender mp3Sender = new TelegramGroupMp3Sender();

        // TODO: Put down group name and group admin below
        string group_name = "Muscle Men Club";
        string group_admin = "19159876123";

        // TODO: Remember to copy the MP3 file from ..\assets to the TEMP directory!
        string base64Content = convertFileToBase64("C:\\TEMP\\ocean-waves.mp3");
        string fn = "anyname.mp3";
        string caption = "Enjoy the nature";
        
        mp3Sender.sendAudio(group_name, group_admin, base64Content, fn, caption);

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

    public bool sendAudio(string group_name, string group_admin, string base64Content, string fn, string caption)
    {
        bool success = true;

        try
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers["X-WM-CLIENT-ID"] = CLIENT_ID;
                client.Headers["X-WM-CLIENT-SECRET"] = CLIENT_SECRET;

                GroupAudioPayload payloadObj = new GroupAudioPayload() { group_name = group_name, group_admin = group_admin, audio = base64Content, filename = fn, caption = caption};
                string postData = (new JavaScriptSerializer()).Serialize(payloadObj);

                client.Encoding = Encoding.UTF8;
                string response = client.UploadString(AUDIO_GROUP_API_URL, postData);
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

    public class GroupAudioPayload
    {
        public string group_name { get; set; }
        public string group_admin { get; set; }
        public string audio { get; set; }
        public string filename { get; set; }
        public string caption { get; set; }
    }

}
