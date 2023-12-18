using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestSpawn : NetworkRequest
{
	public RequestSpawn() {
		request_id = Constants.CMSG_MOVE;
	}

	public void send(int pID, int x, int y, string unitName) {
		packet = new GamePacket(request_id);
        packet.addInt32(pID);
        packet.addInt32(x);
        packet.addInt32(y);
        packet.addString(unitName);
    }
}
