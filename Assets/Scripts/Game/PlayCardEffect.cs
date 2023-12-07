using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardEffect : Effect
{


    private Hex hex;


    public PlayCardEffect(Card card, Hex hex) : base(card)
    {
        this.hex = hex;
        this.Desctiption = card.Owner.Name + " is playing " + card.Name;

        
    }

    public override void EndTurnEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void ImmediateEffect()
    {
        PlayCard.ResolveCard(this.Card, hex);
    }

    public override void Target(GameObject target)
    {
        throw new System.NotImplementedException();
    }

    public override void TargetedEffect(Type typeTargeted)
    {
        throw new System.NotImplementedException();
    }
}
