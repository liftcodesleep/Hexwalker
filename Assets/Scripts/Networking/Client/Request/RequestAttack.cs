using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestAttack : NetworkRequest
{
	public RequestAttack() {
		request_id = Constants.CMSG_ATTACK;
	}

	public void send(int attPid, int attUid, int defPid, int defUid) {
		packet = new GamePacket(request_id);
		packet.addInt32(attPid);
		packet.addInt32(attUid);
		packet.addInt32(defPid);
		packet.addInt32(defPid);
	}
}
