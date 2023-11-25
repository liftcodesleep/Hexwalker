using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingot : Artifact
{
    public Ingot(Player Owner) : base(Owner) {
        Cost = new Charge(0, 1, 0, 0, false);
        Health = 4;
    }

    public override bool IsPlayableHex(Hex hex) {
        throw new System.NotImplementedException();
    }
}
