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
    public int SummonRange = 2;

	public Map.HexType[] moveableHexTypes;

    public Unit(Player Owner) : base(Owner)
    {
           
        type = Card.Type.UNIT;
    }

	public void OnDeath()
    {
        Location = null;
        currentZone = CardZone.Types.GraveYard;
        Owner.AllUnits.Remove(this);
    }

    public int Move(Hex hexTarget)
    {

        if (!ValidMove(hexTarget))
        {
            return 0;
        }

        int hexesTraveled = (int)Location.DistanceFrom(hexTarget);
        Location.cards.Remove(this);

        Location = hexTarget;
        hexTarget.cards.Add(this);

        ActionPoints -= hexesTraveled;

        return hexesTraveled;


    }

    public void Attack(Unit target)
    {
        target.TakeDamage( this.Strength );
    }

    public void TakeDamage(int damage_amount)
    {
        this.Health -= damage_amount;

        if(Health <= 0)
        {
            this.currentZone = CardZone.Types.GraveYard;
            Location = null;
            Debug.Log("Unit was put into the graveyard");
        }
    }


    public override string ToString()
    {

        return base.ToString() +
            "Health: " + Health + "\n" +
            "AttackCost: " + AttackCost + "\n" +
            "MoveCost: " + MoveCost + "\n" +
            "ActionPoints: " + ActionPoints + "\n" +
            "Actions: " + Actions + "\n"+
             "Strength: " + Strength + "\n" ;
    }


    public bool ValidMove(Hex hex)
    {
        bool valid = false;
        if (hex.type == Map.HexType.Water || hex.type == Map.HexType.High)
        {
            return false;
        }

        foreach(Map.HexType currentType in moveableHexTypes)
        {
            if(currentType == hex.type)
            {
                valid = true;
                break;
            }
        }

        if(!valid)
        {
            return false;
        }




        //if (this.Location.DistanceFrom(hex) > this.ActionPoints)
        //{
        //    return false;
        //}
        if (AStartPathfinding.AStartPath(this, this.Location, hex).Count < this.ActionPoints)
        {
            return false;
        }
        return true;
    }
}