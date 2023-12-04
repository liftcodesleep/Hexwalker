using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDeck1 : CardZone {
  public TestDeck1(Player Owner) {
    this.Owner = Owner;
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    this.Cards.Add(new AncestralRecall(this.Owner));
    this.Cards.Add(new Migo(this.Owner));
    Shuffle(this.Cards);
  }
}
