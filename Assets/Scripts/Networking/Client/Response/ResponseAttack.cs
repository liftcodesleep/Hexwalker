using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseAttackEventArgs : ExtendedEventArgs
{
	public int user_id { get; set; } // The user_id of whom who sent the request
	public int piece_idx { get; set; } // The index of the piece that is initiating the interaction. Belongs to player with id user_id
	public int target_idx { get; set; } // The index of the piece that is being interacted with. Belongs to the opponent of the player with id user_id

	public ResponseAttackEventArgs() {
		event_id = Constants.SMSG_ATTACK;
	}
}

public class ResponseAttack : NetworkResponse
{
	private int attPid;
	private int attUid;
	private int defPid;
	private int defUid;

	public ResponseAttack() {
	}

	public override void parse() {
		attPid = DataReader.ReadInt(dataStream);
		attUid = DataReader.ReadInt(dataStream);
		defPid = DataReader.ReadInt(dataStream);
		defUid = DataReader.ReadInt(dataStream);
	}

	public override ExtendedEventArgs process() {
		ResponseAttackEventArgs args = new ResponseAttackEventArgs
		{
			attPid = attPid,
			attUid = attUid,
			defPid = defPid,
			defUid = defUid
		};
		return args;
	}
}
