package Server.src.networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import Server.src.networking.response.GameResponse;
import Server.src.utility.Log;

/**
 * The RequestHeartbeat class is mainly used to release all pending responses
 * the client. Also used to keep the connection alive.
 */
public class RequestHeartbeat extends GameRequest {

  public RequestHeartbeat() {
  }

  @Override
  public void parse() throws IOException {
  }

  @Override
  public void doBusiness() throws Exception {
    for (GameResponse response : client.getUpdates()) {
      try {
        client.send(response);
      } catch (IOException ex) {
        Log.println_e(ex.getMessage());
      }
    }
  }
}
