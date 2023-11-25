using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church : Unit
{


	public Church(Player Owner) : base(Owner) {
		//Charge Cost;
		Name = "Church";
		//string Rules;
		//string Flavor;
		//Priority;
		//Range;
		//IsCard;
		SummonRange = 3;
		HealthPoints = 10;
		Health = 10;
		//AttackCost;
		MoveCost = 999;
		ActionPoints = 0;
		Actions = 0;
		Strength = 0;
		moveableHexTypes = new Map.HexType[] {  };

		type = Card.Type.CHARGE;

		this.Abilities.Add(new ManaTap(this, new Charge(0,0,1,0,false)) );
	}


	public override bool IsPlayableHex(Hex hex) {

		if (hex.type == Map.HexType.Water || hex.type == Map.HexType.High || hex.type == Map.HexType.Forest) {
			return false;
		}

		if (hex.cards.Count > 0) {
			return false;
		}

		if (hex.DistanceFrom(Owner.Avatar.Location) > SummonRange) {

			return false;
		}
		return true;
	}
}
