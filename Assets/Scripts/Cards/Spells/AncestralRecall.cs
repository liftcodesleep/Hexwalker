using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncestralRecall : Spell {
	public AncestralRecall(Player Owner) : base(Owner) {
		// this.Cost = new Charge(1,0,0,0, false);
		Name = "AncestralRecall";
	}

	public override bool IsPlayableHex(Hex hex) {
		if (hex.Constructs.Count > 0) {
      Owner.Draw(3);
			return true;
		}
		return false;
	}
}
