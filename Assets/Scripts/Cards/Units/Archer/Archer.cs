using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit {
  public Archer(Player Owner) : base(Owner) {
    //Charge Cost;
    Name = "Archer";
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
    moveableHexTypes = new Map.HexType[] { Map.HexType.Flat, Map.HexType.Forest };
  }

  public override bool IsPlayableHex(Hex hex) {
    if (hex.type == Map.HexType.Water || hex.type == Map.HexType.High) {
      return false;
    }

    if (hex.Constructs.Count > 0) {
      return false;
    }

    if (hex.DistanceFrom(Owner.Avatar.Location) > SummonRange) {
      return false;
    }
    return true;
  }
}
