package Server.src.networking.request;

import java.io.IOException;

import Server.src.core.NetworkManager;
import model.Player;
import Server.src.networking.response.ResponseSpawn;
import utility.DataReader;

public class RequestSpawn extends GameRequest {
  private int unitID, x, y;
  // Responses
  private ResponseSpawn responseSpawn;

  public RequestSpawn() {
    responses.add(responseSpawn = new ResponseSpawn());
  }

  @Override
  public void parse() throws IOException {
    unitID = DataReader.readInt(dataInput);
    x = DataReader.readInt(dataInput);
    y = DataReader.readInt(dataInput);
  }

  @Override
  public void doBusiness() throws Exception {
    Player player = client.getPlayer();
    responseSpawn.setPlayerID(player);
    responseSpawn.setData(unitID, x, y);
    NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseSpawn);
  }
}
