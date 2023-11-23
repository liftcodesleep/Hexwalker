using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Spell
{
	public FireBall(Player Owner) : base(Owner)
	{
		//Charge Cost;
		Name = "FireBall";
		ETBs.Add(new DamageEffect(this, 9999));

	}


	public override bool IsPlayableHex(Hex hex)
	{

		

		if (hex.cards.Count > 0)
		{
			return true;
        }
        

		
		return false;
	}
}
