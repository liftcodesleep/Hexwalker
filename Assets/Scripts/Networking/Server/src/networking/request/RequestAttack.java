package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseAttack;
import utility.DataReader;
import core.NetworkManager;

public class RequestAttack extends GameRequest {
    private int pieceIndex, targetIndex;
    // Responses
    private ResponseAttack responseAttack;

    public RequestAttack() {
        responses.add(responseAttack = new ResponseAttack());
    }

    @Override
    public void parse() throws IOException {
        pieceIndex = DataReader.readInt(dataInput);
        targetIndex = DataReader.readInt(dataInput);
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();
        responseAttack.setPlayer(player);
        responseAttack.setData(pieceIndex, targetIndex);
        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseAttack);
    }
}
