using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Unit
{

	

	public Knight(Player Owner) : base(Owner) {
		//Charge Cost;
		Name = "Knight";
		//string Rules;
		//string Flavor;
		//Priority;
		//Range;
		//IsCard;

		HealthPoints = 4;
		Health = 4;
		//AttackCost;
		MoveCost = 2;
		ActionPoints = 2;
		Actions = 2;
		Strength = 1;
		moveableHexTypes = new Map.HexType[]{ Map.HexType.Flat, Map.HexType.Forest };

		
	}


	public override bool IsPlayableHex(Hex hex) {
		
		if(hex.type == Map.HexType.Water || hex.type == Map.HexType.High) {
			return false;
        }

		if(hex.cards.Count > 0) {
			return false;
        }

		if (hex.DistanceFrom(Owner.Avatar.Location) > SummonRange) {

			return false;
		}
		return true;
	}
}
