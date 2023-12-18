package networking.response;

import metadata.Constants;
import model.Player;
import utility.GamePacket;
import utility.Log;

public class ResponseSpawn extends GameResponse {
  Player playerID;
  private int pID;
  private int x;
  private int y;
  private int cardID;

  public ResponseSpawn() {
    responseCode = Constants.SMSG_SPAWN;
  }

  @Override
  public byte[] constructResponseInBytes() {
    GamePacket packet = new GamePacket(responseCode);
    packet.addInt32(pID);
    packet.addInt32(x);
    packet.addInt32(y);
    packet.addInt32(cardID);
    Log.printf("Player with ID %d has Spawned piece %d at (%d, %d)", pID, cardID, x, y);
    return packet.getBytes();
  }

  public void setPlayerID(Player playerID) {
    this.playerID = playerID;
  }

  public void setData(int pID, int x, int y, int cardID) {
    this.pID = pID;
    this.y = y;
    this.x = x;
    this.cardID = cardID;
  }
}
