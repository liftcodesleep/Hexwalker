using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell {
	public Fireball(Player Owner) : base(Owner) {
		//Charge Cost;
		Name = "Fireball";
		ETBs.Add(new DamageEffect(this, 9999));
	}

	public override bool IsPlayableHex(Hex hex) {
		if (hex.Constructs.Count > 0) {
			return true;
		}
		return false;
	}
}
