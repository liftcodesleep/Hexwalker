using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string Name { get; set; }
    public CardZone Hand { get; set; }
    public CardZone Deck { get; set; }
    public CardZone Graveyard { get; set; }
    // public List<Unit> army { get; }
    // public List<Effect> effects { get; }
    public List<Power> Bank;
    public int Attunement;
    public int LandPlays;
    public int LandsPlayed;

    public Player()
    {
        this.Deck = new CardZone();
        this.Graveyard = new CardZone();
        this.Hand = new CardZone();
        this.Bank = new List<Power>();
        Hand.Capacity = 6;
    }

    public int GainPower(Power power)
    {
        this.Bank.Add(power);
        return 0;
    }


    private int CanPlayLand()
    {
        return LandPlays - LandsPlayed;
    }
}