using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDeck2 : CardZone {
  public TestDeck2(Player Owner) {
    this.Owner = Owner;
    this.Cards.Add(new GiantGrowth(this.Owner));
    Shuffle(this.Cards);
  }
}
