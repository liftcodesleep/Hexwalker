using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Migo : Unit {
	public Migo(Player Owner) : base(Owner) {
		// this.Cost = new Charge(1,0,0,0, false);
		Name = "Migo";
		HealthPoints = 6;
		Health = 6;
		//AttackCost;
		MoveCost = 1;
		ActionPoints = 3;
		Actions = 4;
		Strength = 3;
		moveableHexTypes = new Map.HexType[] { Map.HexType.Flat, Map.HexType.Forest, Map.HexType.Water, Map.HexType.High };
	}

	public override bool IsPlayableHex(Hex hex) {
		if (hex.Constructs.Count > 0) {
			return false;
		}
		if (hex.DistanceFrom(Owner.Avatar.Location) > SummonRange) {
			return false;
		}
		return true;
	}
}
