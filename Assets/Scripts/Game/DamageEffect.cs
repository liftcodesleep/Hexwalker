using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : Effect
{

    private int damageAmount;

    public DamageEffect(Card card, int damageAmount) : base(card)
    {
        EffectType = Type.MOVE;
        this.damageAmount = damageAmount;
    }


    public override void EndTurnEffect()
    {

    }

    public override void Target(GameObject target)
    {
        throw new System.NotImplementedException();
    }

    public override void ImmediateEffect()
    {
        Debug.Log("Damage Effect");
        if (Card.Location == null)
        {
            Debug.Log("No targets on hex");
            return;
        }
        Debug.Log("Damage End");
        foreach (Card currentCard in Card.Location.cards)
        {

            Unit currentUnit;
            try
            {
                currentUnit = (Unit)currentCard;
            }
            catch
            {
                continue;
            }

            Debug.Log("name is");
            Debug.Log(currentUnit.Name);
            Debug.Log("000oo");
            if (currentUnit != null)
            {
                Debug.Log("AAAAAAAAAAAAAAAA");
            }
            currentUnit.TakeDamage( damageAmount );
        }
        

        PutOnStack(this);
        
    }

    public override void TargetedEffect(Type typeTargeted)
    {

    }
}
