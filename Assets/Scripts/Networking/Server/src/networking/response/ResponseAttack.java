package networking.response;

// Other Imports
import metadata.Constants;
import model.Player;
import utility.GamePacket;
import utility.Log;

/**
 * The ResponseLogin class contains information about the authentication
 * process.
 */
public class ResponseAttack extends GameResponse {
    private Player player;
    private int attPid, defPid, attUid, defUid;

    public ResponseAttack() {
        responseCode = Constants.SMSG_ATTACK;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(attPid);
        packet.addInt32(defPid);
        packet.addInt32(attUid);
        packet.addInt32(defUid);

        Log.printf("Player with id %d has had a piece at id %d attack another player's piece at id %d.",
                player.getID(), attUid, defUid);

        return packet.getBytes();
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public void setData(int attPid, int defPid, int attUid, int defUid) {
        this.attPid = attPid;
        this.defPid = defPid;
        this.attUid = attUid;
        this.defUid = defUid;
    }
}
