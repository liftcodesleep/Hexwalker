package networking.request;

import java.io.IOException;
import core.NetworkManager;
import model.Player;
import networking.response.ResponseSpawn;
import utility.DataReader;

public class RequestSpawn extends GameRequest {
  private int pID;
  private int x;
  private int y;
  private int cardID;
  // Responses
  private ResponseSpawn responseSpawn;

  public RequestSpawn() {
    responses.add(responseSpawn = new ResponseSpawn());
  }

  @Override
  public void parse() throws IOException {
    pID = DataReader.readInt(dataInput);
    x = DataReader.readInt(dataInput);
    y = DataReader.readInt(dataInput);
    cardID = DataReader.readInt(dataInput);
  }

  @Override
  public void doBusiness() throws Exception {
    Player player = client.getPlayer();
    responseSpawn.setPlayerID(player);
    responseSpawn.setData(pID, x, y, cardID);
    NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseSpawn);
  }
}
