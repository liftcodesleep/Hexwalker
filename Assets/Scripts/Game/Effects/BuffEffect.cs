using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffect : Effect {
    Unit currentUnit;
    private int heathAmount;
    private int damageAmount;
    private int actionsAmount;
    private int turns_left;

    public BuffEffect(Card card, int heathAmount, int damageAmount, int actionsAmount, int turns) : base(card) {
        EffectType = Type.MOVE;
        this.heathAmount = heathAmount;
        this.damageAmount = damageAmount;
        this.actionsAmount = actionsAmount;
        this.turns_left = turns;
    }

    public override void EndTurnEffect()
    {
        turns_left--;
        if (turns_left <= 0)
        {
            currentUnit.Strength -= this.damageAmount;
            currentUnit.Health -= this.heathAmount;
            currentUnit.ActionPoints -= this.actionsAmount;
            currentUnit.ActiveEffects.Remove(this);
        }
    }

    public override void Target(GameObject target) {
        throw new System.NotImplementedException();
    }

    public override void ImmediateEffect() {
        
        if (Card.Location == null) {
            Debug.Log("No targets on hex");
            return;
        }
        
        foreach (Construct currentConstruct in Card.Location.Constructs) {
            try {
                currentUnit = (Unit)currentConstruct;
            }
            catch {
                continue;
            }
            //Debug.Log("name is");
            Debug.Log(currentUnit.Name);
            //Debug.Log("000oo");
            if (currentUnit != null) {
                //Debug.Log("AAAAAAAAAAAAAAAA");
            }
            currentUnit.Strength += this.damageAmount;
            currentUnit.Health += this.heathAmount;
            currentUnit.ActionPoints += this.actionsAmount;
            currentUnit.ActiveEffects.Add(this);
            break;
        }
        //PutOnStack(this);
    }

    public override void TargetedEffect(Type typeTargeted) {

    }
}
