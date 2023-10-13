using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaTap : Effect
{

    private Charge charge;

    
    public ManaTap(Card card, Charge charge) : base(card)
    {
        this.charge = charge;

        this.Name = "Tap Mana";
        this.Desctiption = "Addes mana to players mana pool";
    }

    public override void EndTurnEffect()
    {
        //this.Card.Owner.c GainCharge(charge);

    }

    public override void ImmediateEffect()
    {
        //this.Card.Owner.GainCharge(charge);

        this.Card.Owner.Pool += charge;
        Card.Tapped = true;
        this.Resolved = true;

        Debug.Log("Added mana, Player mana now: " + Game.players[0].Pool.Holy );
    }

    public override void TargetedEffect(Type typeTargeted)
    {
        
    }
}
