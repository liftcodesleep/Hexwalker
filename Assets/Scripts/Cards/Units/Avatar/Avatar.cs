using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : Unit
{
	public Avatar(Player Owner) : base(Owner) {
		//Charge Cost;
		Name = "Avatar";
		//string Rules;
		//string Flavor;
		//Priority;
		//Range;
		//IsCard;

		HealthPoints = 20;
		Health = 20;
		//AttackCost;
		MoveCost = 2;
		ActionPoints = 2;
		Actions = 2;
		Strength = 0;
		moveableHexTypes = new Map.HexType[] { Map.HexType.Flat, Map.HexType.Forest };

	}


	public override bool IsPlayableHex(Hex hex) {

		if (hex.type == Map.HexType.Water || hex.type == Map.HexType.High) {
			return false;
		}

		if (hex.cards.Count > 0) {
			return false;
		}
		return true;
	}
}
