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
 *   javac -cp "jars/gson-2.8.0.jar:jars/commons-codec-1.10.jar" TelegramOpusSender.java 
 *   
 * Then, run the class using this command:
 *   java -cp ".:jars/gson-2.8.0.jar:jars/commons-codec-1.10.jar" TelegramOpusSender
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


public class TelegramOpusSender {
    /**
     * Inner class that captures the information needed to construct the JSON object
     * for sending a voice note message.
     */
    class VoiceNoteMessage {
        String number = null;
        String voice_note = null;
        String filename = null;
        String caption = null;
    }
    
    // TODO: Replace the following with your gateway instance ID, Premium Account
    // Client ID and Secret below.
    private static final String INSTANCE_ID = "YOUR_INSTANCE_ID_HERE";
    private static final String CLIENT_ID = "YOUR_CLIENT_ID_HERE";
    private static final String CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE";

    private static final String GATEWAY_URL = "https://api.whatsmate.net/v3/telegram/single/voice_note/message/" + INSTANCE_ID;

    /**
     * Entry Point
     */
    public static void main(String[] args) throws Exception {
        // TODO: Specify the recipients of your image 
        String recipient = "1234556899";
        // TODO: Specify the content of your OPUS file
        Path audioPath = Paths.get("../assets/martin-luther-king.opus");
        byte[] audioBytes = Files.readAllBytes(audioPath);
        String filename = "anyname.opus";
        String caption = "I have a dream";
        
        TelegramOpusSender opusSender = new TelegramOpusSender();
        opusSender.sendVoiceNoteMessage(recipient, audioBytes, filename, caption);
    }

    /**
     * Sends out a Telegram message (a voice note file) to a person
     */
    public void sendVoiceNoteMessage(String recipient, byte[] audioBytes, String filename, String caption)
            throws Exception {
        byte[] encodedBytes = Base64.encodeBase64(audioBytes);
        String base64Audio = new String(encodedBytes);
        
        VoiceNoteMessage voiceNoteMsgObj = new VoiceNoteMessage();
        voiceNoteMsgObj.number = recipient;
        voiceNoteMsgObj.voice_note = base64Audio;
        voiceNoteMsgObj.filename = filename;
        voiceNoteMsgObj.caption = caption;

        Gson gson = new Gson();
        String jsonPayload = gson.toJson(voiceNoteMsgObj);

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

