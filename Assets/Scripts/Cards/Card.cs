using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public Charge Cost;
    public string Name;
    public string Rules;
    public string Flavor;
    public int Priority;
    public int Range;
    public int IsCard;
    public Hex Location;
    public Player Owner;

    public List<GameObject> Pieces;

    public Card(Player Owner)
    {
        this.Owner = Owner;
        Pieces = new List<GameObject>();
    }

    public enum Type { CHARGE, UNIT, ARTIFACT, SPELL  }

    public Type type;

    public abstract bool IsPlayableHex(Hex hex);


    public override string ToString() 
    {
        
        return "Name: " + Name + "\n" +
            "Range: " + Range + "\n";
    }


}