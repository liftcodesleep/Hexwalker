package Server.src.networking.response;

// Other Imports
import Server.src.metadata.Constants;
import Server.src.model.Player;
import Server.src.utility.GamePacket;
import Server.src.utility.Log;

/**
 * The ResponseLogin class contains information about the authentication
 * process.
 */
public class ResponseName extends GameResponse {
  private Player player;

  public ResponseName() {
    responseCode = Constants.SMSG_SETNAME;
  }

  @Override
  public byte[] constructResponseInBytes() {
    GamePacket packet = new GamePacket(responseCode);
    packet.addInt32(player.getID());
    packet.addString(player.getName());

    Log.printf("Name %s set in server. for player with id %d", player.getName(), player.getID());

    return packet.getBytes();
  }

  public void setPlayer(Player player) {
    this.player = player;
  }
}