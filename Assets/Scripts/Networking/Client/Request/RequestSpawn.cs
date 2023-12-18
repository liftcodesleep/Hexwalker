using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestSpawn : NetworkRequest
{
	public RequestSpawn() {
		request_id = Constants.CMSG_SPAWN;
	}

	public void send(int pID, int x, int y, int cardID) {
		packet = new GamePacket(request_id);
        packet.addInt32(pID);
        packet.addInt32(x);
        packet.addInt32(y);
        packet.addInt32(cardID);
    }
}
