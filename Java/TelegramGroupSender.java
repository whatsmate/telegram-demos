/*
 * If you use Maven, add the following to your pom.xml:
 *   <dependency>
 * 		<groupId>com.google.code.gson</groupId>
 *		<artifactId>gson</artifactId>
 *		<version>2.8.0</version>
 *   </dependency>
 *  
 *  
 * If you don't use Maven, compile this class using this command: 
 *   javac -cp "jars/gson-2.8.0.jar" TelegramGroupSender.java 
 *   
 * Then, run the class using this command:
 *   java -cp ".:jars/gson-2.8.0.jar" TelegramGroupSender
 */

import java.io.BufferedReader;
import java.io.OutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

import com.google.gson.Gson;


public class TelegramGroupSender {
	/**
	 * Inner class that captures the information needed to construct the JSON object
	 * for sending a group message.
	 */
	class GroupMessage {
		String group = null;
		String message = null;
	}

	// TODO: Replace the following with your gateway instance ID, Forever Green
	// Client ID and Secret below.
	private static final String INSTANCE_ID = "YOUR_INSTANCE_ID_HERE";
	private static final String CLIENT_ID = "YOUR_CLIENT_ID_HERE";
	private static final String CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE";

	private static final String GATEWAY_URL = "http://api.whatsmate.net/v1/telegram/group/message/"
			+ INSTANCE_ID;

	/**
	 * Entry Point
	 */
	public static void main(String[] args) throws Exception {
		// TODO: Specify the name of the group
		String groupName = "YOUR_UNIQUE_GROUP_NAME_GOES_HERE"; 
		// TODO: Specify the content of your message
		String message = "Guys, let's party tonight!"; 
		TelegramGroupSender groupSender = new TelegramGroupSender();
		groupSender.sendGroupMessage(groupName, message);
	}

	/**
	 * Sends out a Telegram message to a group
	 */
	public void sendGroupMessage(String groupName, String message)
			throws Exception {
		
		GroupMessage groupMsgObj = new GroupMessage();
		groupMsgObj.group = groupName;
		groupMsgObj.message = message;

		Gson gson = new Gson();
		String jsonPayload = gson.toJson(groupMsgObj);

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

