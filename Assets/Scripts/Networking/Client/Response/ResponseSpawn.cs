using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseSpawnEventArgs : ExtendedEventArgs {
	public int pID { get; set; } // The user_id of whom who sent the request
	public int x { get; set; } // The x coordinate of the target location
	public int y { get; set; } // The y coordinate of the target location
	public int cardID { get; set; }

	public ResponseSpawnEventArgs() {
		event_id = Constants.SMSG_SPAWN;
	}
}

public class ResponseSpawn : NetworkResponse {
	private int pID;
	private int x;
	private int y;
	private int cardID;

	public ResponseSpawn() {
	}

	public override void parse() {
		pID = DataReader.ReadInt(dataStream);
		x = DataReader.ReadInt(dataStream);
		y = DataReader.ReadInt(dataStream);
		cardID = DataReader.ReadInt(dataStream);
	}

	public override ExtendedEventArgs process() {
		ResponseSpawnEventArgs args = new ResponseSpawnEventArgs {
			pID = pID,
			x = x,
			y = y,
			cardID = cardID
		};
		return args;
	}
}
