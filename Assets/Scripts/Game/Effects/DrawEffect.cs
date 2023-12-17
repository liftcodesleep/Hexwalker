using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawEffect : Effect {
    private int numCards;
    
    public DrawEffect(Card card, int numCards) :base(card){}

    public override void Target(GameObject target){
        //TODO
    }

    public override void ImmediateEffect() {
        // this.Card.Owner.Draw(3);
    }

    public override void EndTurnEffect(){
        //TODO
    }

    public override void TargetedEffect(Type typeTargeted){
        //TODO
    }
}
