using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellComponent : MonoBehaviour {
  public Spell spell;
  void Start() {
  }

  // Update is called once per frame
  void Update() {
    if(spell.currentZone == CardZone.Types.Graveyard) {
      Destroy(this);
    }
  }
}
