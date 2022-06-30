/*
 * If you use Maven, add the following to your pom.xml:
 *   <dependency>
 *              <groupId>com.google.code.gson</groupId>
 *              <artifactId>gson</artifactId>
 *              <version>2.8.0</version>
 *   </dependency>
 *   <dependency>
 *          <groupId>commons-codec</groupId>
 *          <artifactId>commons-codec</artifactId>
 *          <version>1.10</version>
 *   </dependency>
 *  
 *  
 * If you don't use Maven, compile this class using this command: 
 *   javac -cp "jars/gson-2.8.0.jar:jars/commons-codec-1.10.jar" TelegramGroupPhotoSender.java 
 *   
 * Then, run the class using this command:
 *   java -cp ".:jars/gson-2.8.0.jar:jars/commons-codec-1.10.jar" TelegramGroupPhotoSender
 */

import java.io.BufferedReader;
import java.io.OutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

import org.apache.commons.codec.binary.Base64;

import com.google.gson.Gson;


public class TelegramGroupPhotoSender {
    /**
     * Inner class that captures the information needed to construct the JSON object
     * for sending an image message.
     */
    class ImageMessage {
        String group_name = null;
        String group_admin = null;
        String caption = null;
        String image = null;
    }
    
    // TODO: Replace the following with your gateway instance ID, Premium Account
    // Client ID and Secret below.
    private static final String INSTANCE_ID = "YOUR_INSTANCE_ID_HERE";
    private static final String CLIENT_ID = "YOUR_CLIENT_ID_HERE";
    private static final String CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE";

    private static final String GATEWAY_URL = "https://api.whatsmate.net/v3/telegram/group/image/message/" + INSTANCE_ID;

    /**
     * Entry Point
     */
    public static void main(String[] args) throws Exception {
        String group_name = "Muscle Men Club";  //  TODO: Specify the group name here.
        String group_admin = "19159876123";     //  TODO: Specify the number of the group admin here.

        // TODO: Specify the content of your image
        Path imagePath = Paths.get("../assets/cute-girl.jpg");
        byte[] imageBytes = Files.readAllBytes(imagePath);
        String caption = "Lovely Girl";
        
        TelegramGroupPhotoSender imgSender = new TelegramGroupPhotoSender();
        imgSender.sendPhotoMessage(group_name, group_admin, imageBytes, caption);
    }

    /**
     * Sends out a Telegram message (an image) to a group
     */
    public void sendPhotoMessage(String group_name, String group_admin, byte[] imageBytes, String caption)
            throws Exception {
        byte[] encodedBytes = Base64.encodeBase64(imageBytes);
        String base64Image = new String(encodedBytes);
        
        ImageMessage imageMsgObj = new ImageMessage();
        imageMsgObj.group_name = group_name;
        imageMsgObj.group_admin = group_admin;
        imageMsgObj.image = base64Image;
        imageMsgObj.caption = caption;

        Gson gson = new Gson();
        String jsonPayload = gson.toJson(imageMsgObj);

        URL url = new URL(GATEWAY_URL);
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
        System.out.println("Response from Telegram Gateway: \n");
        System.out.println("Status Code: " + statusCode);
        BufferedReader br = new BufferedReader(new InputStreamReader(
                (statusCode == 200) ? conn.getInputStream()
                        : conn.getErrorStream()));
        String output;
        while ((output = br.readLine()) != null) {
            System.out.println(output);
        }
        conn.disconnect();
    }

}