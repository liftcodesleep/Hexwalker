using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseSpawnEventArgs : ExtendedEventArgs {
	public int pID { get; set; } // The user_id of whom who sent the request
	public int x { get; set; } // The x coordinate of the target location
	public int y { get; set; } // The y coordinate of the target location
	public string unitName { get; set; }

	public ResponseSpawnEventArgs() {
		event_id = Constants.SMSG_MOVE;
	}
}

public class ResponseSpawn : NetworkResponse {
	private int pID;
	private int x;
	private int y;
	private string unitName;

	public ResponseSpawn() {
	}

	public override void parse() {
		pID = DataReader.ReadInt(dataStream);
		x = DataReader.ReadInt(dataStream);
		y = DataReader.ReadInt(dataStream);
		unitName = DataReader.ReadString(dataStream);
	}

	public override ExtendedEventArgs process() {
		ResponseSpawnEventArgs args = new ResponseSpawnEventArgs {
			pID = pID,
			x = x,
			y = y,
			unitName = unitName
		};
		return args;
	}
}
