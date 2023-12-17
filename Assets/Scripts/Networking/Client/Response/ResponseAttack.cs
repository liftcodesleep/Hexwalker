using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseAttackEventArgs : ExtendedEventArgs
{
	public int user_id { get; set; } // 1 is first to join, 2 is second, anything else is not valid!
	public int attPid { get; set; } // The user_id of the attacking player
	public int attUid { get; set; } // The index of the attacking unit
	public int defPid { get; set; } // The index of the defending player
	public int defUid { get; set; } // The index of the defending unit

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
