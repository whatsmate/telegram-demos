import java.io.BufferedReader;
import java.io.OutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class TelegramGroupTextSender {
  // TODO: Replace the following with your instance ID, Premium Account Client ID and Secret:
  private static final String INSTANCE_ID = "YOUR_INSTANCE_ID_HERE";
  private static final String CLIENT_ID = "YOUR_CLIENT_ID_HERE";
  private static final String CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE";
  private static final String TG_GATEWAY_URL = "https://api.whatsmate.net/v3/telegram/group/text/message/" + INSTANCE_ID;

  /**
   * Entry Point
   */
  public static void main(String[] args) throws Exception {
    String group_name = "Muscle Men Club";  //  TODO: Specify the group name here.
    String group_admin = "19159876123";     //  TODO: Specify the number of the group admin here.
    String message = "Your six-pack is on the way!";

    TelegramGroupTextSender.sendGroupMessage(group_name, group_admin, message);
  }

  /**
   * Sends out a group message via WhatsMate Telegram Gateway.
   */
  public static void sendGroupMessage(String group_name, String group_admin, String message) throws Exception {
    // TODO: Should have used a 3rd party library to make a JSON string from an object
    String jsonPayload = new StringBuilder()
      .append("{")
      .append("\"group_name\":\"")
      .append(group_name)
      .append("\",")
      .append("\"group_admin\":\"")
      .append(group_admin)
      .append("\",")
      .append("\"message\":\"")
      .append(message)
      .append("\"")
      .append("}")
      .toString();

    URL url = new URL(TG_GATEWAY_URL);
    HttpURLConnection conn = (HttpURLConnection) url.openConnection();
    conn.setDoOutput(true);
    conn.setRequestMethod("POST");
    conn.setRequestProperty("X-WM-CLIENT-ID", CLIENT_ID);
    conn.setRequestProperty("X-WM-CLIENT-SECRET", CLIENT_SECRET);
    conn.setRequestProperty("Content-Type", "application/json");

    OutputStream os = conn.getOutputStream();
    os.write(jsonPayload.getBytes());
    os.flush();
    os.close();

    int statusCode = conn.getResponseCode();
    System.out.println("Response from WA Gateway: \n");
    System.out.println("Status Code: " + statusCode);
    BufferedReader br = new BufferedReader(new InputStreamReader(
        (statusCode == 200) ? conn.getInputStream() : conn.getErrorStream()
      ));
    String output;
    while ((output = br.readLine()) != null) {
        System.out.println(output);
    }
    conn.disconnect();
  }

}