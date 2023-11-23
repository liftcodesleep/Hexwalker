package networking.response;

import metadata.Constants;
import model.Player;
import utility.GamePacket;
import utility.Log;

public class ResponseSpawn extends GameResponse {
  private Player playerID;
  private int x;
  private int y;
  private int unitID;

  public ResponseSpawn() {
    responseCode = Constants.SMSG_SPAWN;
  }

  @Override
  public byte[] constructResponseInBytes() {
    GamePacket packet = new GamePacket(responseCode);
    packet.addInt32(playerID.getID());
    packet.addInt32(unitID);
    packet.addInt32(x);
    packet.addInt32(y);
    Log.printf("Player with ID %d has Spawned piece %d at (%d, %d)", playerID.getID(), unitID, x, y);
    return packet.getBytes();
  }

  public void setPlayerID(Player playerID) {
    this.playerID = playerID;
  }

  public void setData(int unitID, int x, int y) {
    this.unitID = unitID;
    this.y = y;
    this.x = x;
  }
}
