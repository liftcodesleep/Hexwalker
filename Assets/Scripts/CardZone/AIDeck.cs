using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeck : CardZone
{

    public AIDeck(Player Owner)
    {
        this.Owner = Owner;

        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Knight(this.Owner));
    }

}
