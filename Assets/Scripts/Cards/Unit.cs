using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : Construct
{
	//List<Effect> UnitEffects;
	public int HealthPoints = 1;
	public int Health = 1;
	public int AttackCost = 1;
	public int MoveCost = 1;
	public int ActionPoints = 1;
	public int Actions = 1;
	public int Strength = 1;

	public Map.HexType[] moveableHexTypes;

	public void OnDeath()
    {

    }

    public int Move(Hex hexTarget)
    {

        if (!this.IsPlayableHex(hexTarget))
        {
            return 0;
        }

        int hexesTraveled = (int)Location.DistanceFrom(hexTarget);
        Location.cards.Remove(this);


        Location = hexTarget;

        hexTarget.cards.Add(this);

        return hexesTraveled;


    }
}