using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawEffect : Effect {
    public abstract void Target(GameObject target);

    public abstract void ImmediateEffect() {
        Owner.Draw(3);
    }

    public abstract void EndTurnEffect();

    public abstract void TargetedEffect(Type typeTargeted);
}
