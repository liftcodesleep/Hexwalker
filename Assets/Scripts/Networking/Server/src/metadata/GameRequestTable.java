package Server.src.metadata;

// Java Imports
import java.util.HashMap;
import java.util.Map;

// Other Imports
import Server.src.networking.request.GameRequest;
import Server.src.utility.Log;

/**
 * The GameRequestTable class stores a mapping of unique request code numbers
 * with its corresponding request class.
 */
public class GameRequestTable {

  private static Map<Short, Class<? extends GameRequest>> requestTable = new HashMap<Short, Class<? extends GameRequest>>(); // Request
                                                                                                                             // Code
                                                                                                                             // ->
                                                                                                                             // Class

  /**
   * Initialize the hash map by populating it with request codes and classes.
   */
  public static void init() {
    // Populate the table using request codes and class names
    add(Constants.CMSG_JOIN, "RequestJoin");
    add(Constants.CMSG_LEAVE, "RequestLeave");
    add(Constants.CMSG_SETNAME, "RequestName");
    add(Constants.CMSG_READY, "RequestReady");
    add(Constants.CMSG_MOVE, "RequestMove");
    add(Constants.CMSG_INTERACT, "RequestInteract");
    add(Constants.CMSG_SPAWN, "RequestSpawn");
    add(Constants.CMSG_HEARTBEAT, "RequestHeartbeat");
  }

  /**
   * Map the request code number with its corresponding request class, derived
   * from its class name using reflection, by inserting the pair into the
   * table.
   *
   * @param code a value that uniquely identifies the request type
   * @param name a string value that holds the name of the request class
   */
  public static void add(Short code, String name) {
    try {
      Class<?> rawRequestClass = Class.forName("networking.request." + name);
      if (GameRequest.class.isAssignableFrom(rawRequestClass)) {
        @SuppressWarnings("unchecked")
        Class<? extends GameRequest> requestClass = (Class<? extends GameRequest>) rawRequestClass;

        // Now you can use requestClass safely
        requestTable.put(code, requestClass);
      }
    } catch (ClassNotFoundException e) {
      Log.println_e(e.getMessage());
    }
  }

  /**
   * Get the instance of the request class by the given request code.
   *
   * @param request_code a value that uniquely identifies the request type
   * @return the instance of the request class
   */
  public static GameRequest get(short request_code) {
    GameRequest request = null;

    try {
      Class<? extends GameRequest> name = requestTable.get(request_code);

      if (name != null) {
        // Added getDeclaredConstructor() per Java 9, unsure of implications
        request = (GameRequest) name.getDeclaredConstructor().newInstance();
        request.setID(request_code);
      } else {
        Log.printf_e("Request Code [%d] does not exist!\n", request_code);
      }
    } catch (Exception e) {
      Log.println_e(e.getMessage());
    }

    return request;
  }
}
