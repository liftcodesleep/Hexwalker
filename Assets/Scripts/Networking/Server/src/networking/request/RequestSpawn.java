package networking.request;

import java.io.IOException;

import core.NetworkManager;
import model.Player;
import networking.response.ResponseSpawn;
import utility.DataReader;

public class RequestSpawn extends GameRequest {
    private int pID, x, y;
    private String unitName;

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
    unitName = DataReader.readString(dataInput);
  }

  @Override
  public void doBusiness() throws Exception {
    Player player = client.getPlayer();
    responseSpawn.setPlayerID(player);
    responseSpawn.setData(pID, x, y, unitName);
    NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseSpawn);
  }
}
