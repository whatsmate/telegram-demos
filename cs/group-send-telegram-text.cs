using System;
using System.Net;
using System.Web.Script.Serialization; // requires the reference 'System.Web.Extensions'
using System.IO;
using System.Text;

class TelegramGroupMessageSender
{
    // TODO: Replace the following with your gateway instance ID, Premium Account client ID and secret:
    private static string INSTANCE_ID = "YOUR_INSTANCE_ID_HERE";
    private static string CLIENT_ID = "YOUR_CLIENT_ID_HERE";
    private static string CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE";

    private static string API_URL = "https://api.whatsmate.net/v3/telegram/group/text/message/" + INSTANCE_ID;

    static void Main(string[] args)
    {
        TelegramGroupMessageSender groupMsgSender = new TelegramGroupMessageSender();
        groupMsgSender.sendGroupMessage("Muscle Men Club", "19159876123", "Your six-pack is on the way!");  // TODO: Specify the group namd and group admin
        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }

    public bool sendGroupMessage(string group_name, string group_admin, string message)
    {
        bool success = true;

        try
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers["X-WM-CLIENT-ID"] = CLIENT_ID;
                client.Headers["X-WM-CLIENT-SECRET"] = CLIENT_SECRET;

                GroupPayload payloadObj = new GroupPayload() { group_name = group_name, group_admin = group_admin, message = message };
                string postData = (new JavaScriptSerializer()).Serialize(payloadObj);

                client.Encoding = Encoding.UTF8;
                string response = client.UploadString(API_URL, postData);
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

    public class GroupPayload
    {
        public string group_name { get; set; }
        public string group_admin { get; set; }
        public string message { get; set; }
    }

}