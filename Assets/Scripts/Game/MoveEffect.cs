using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEffect : Effect
{

    public MoveEffect(Card card): base(card)
    {
        EffectType = Type.MOVE;
    }


    public override void EndTurnEffect()
    {
        
    }

    public override void ImmediateEffect()
    {

        Unit movedUnit = (Unit)Card;

        if (movedUnit == null)
        {
            throw new Exception(Card.Name  + "Has a MoveEffect that is not a unit");
        }

        movedUnit.Move(new Hex(1,1));
        PutOnStack(this);

    }

    public override void TargetedEffect(Type typeTargeted)
    {
        
    }

    public override void Target(GameObject target)
    {
        throw new System.NotImplementedException();
    }
}
