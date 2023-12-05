using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect {
  public string Name;
  public string Desctiption;
  public int NumberOfTargets;
  public List<GameObject> targets;
  public Card Card;
  public bool Resolved;
  public float ResolveTime;
  public enum Type { ATTACK, DAMAGE, MOVE };
  public Type EffectType; 

  public Effect(Card card) {
    this.Card = card;
    Resolved = false;
    targets = new List<GameObject>();
    ResolveTime = 100f;
  }

  public abstract void Target(GameObject target);

  public static void PutOnStack(Effect effect) {
    Effect copyedEffect = (Effect)effect.MemberwiseClone();
    Game.stack.Push(copyedEffect );
  }

  public abstract void ImmediateEffect();

  public abstract void EndTurnEffect();

  public abstract void TargetedEffect(Type typeTargeted);
}
