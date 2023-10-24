using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect 
{

    

    public string Name;
    public string Desctiption;

    public int NumberOfTargets;

    public Card Card;
    public bool Resolved;

    public enum Type { ATTACK, DAMAGE, MOVE };
    public Type EffectType; 

    public Effect(Card card)
    {
        this.Card = card;
        Resolved = false;
    }

    //public void Target();


    public static void PutOnStack(Effect effect)
    {
        Effect copyedEffect = (Effect)effect.MemberwiseClone();

        
        Game.stack.Push(copyedEffect );
    }

    public abstract void ImmediateEffect();


    public abstract void EndTurnEffect();


    public abstract void TargetedEffect(Type typeTargeted);

}
