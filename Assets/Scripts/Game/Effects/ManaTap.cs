using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaTap : Effect {
  private Charge charge;

  public ManaTap(Card card, Charge charge) : base(card) {
    this.charge = charge;
    this.Name = "Tap Mana";
    this.Desctiption = "Adds mana to players mana pool";
    this.NumberOfTargets = 0;
  }
  public override void EndTurnEffect() {
  }

  public override void ImmediateEffect() {
    this.Card.Owner.Pool += charge;
    Card.Tapped = true;
    this.Resolved = true;
    Debug.Log("Added mana, Player mana now: " + Game.players[0].Pool.Holy );
  }

  public override void TargetedEffect(Type typeTargeted) {
  }

  public override void Target(GameObject target) {
    throw new System.NotImplementedException();
  }
}
