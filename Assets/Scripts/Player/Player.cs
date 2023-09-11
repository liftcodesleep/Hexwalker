using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string Name { get; set; }
    public CardZone hand { get; set; }
    public CardZone deck { get; set; }
    public CardZone graveyard { get; set; }
    // public List<Unit> army { get; }
    // public List<Effect> effects { get; }
    // TODO mana
    public int landPlays;
    public int landsPlayed;

    public Player()
    {
        this.hand = new CardZone();
        hand.owner = this;
        hand.Capacity = 7;
    }

    private int canPlayLand()
    {
        return landPlays - landsPlayed;
    }


}