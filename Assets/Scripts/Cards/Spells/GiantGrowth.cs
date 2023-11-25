using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantGrowth : Spell
{
	public GiantGrowth(Player Owner) : base(Owner) {
		//Charge Cost;
		Name = "GiantGrowth";
		ETBs.Add(new BuffEffect(this, 3,3,0,1));

	}


	public override bool IsPlayableHex(Hex hex) {

		

		if (hex.cards.Count > 0) {
			return true;
        }
        

		
		return false;
	}
}
