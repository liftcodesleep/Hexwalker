using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightsDeck : CardZone
{
    
    //public KnightsDeck(Player Owner)
    //{
    //    this.Owner = Owner;
    //}

    public KnightsDeck(Player Owner) {
        this.Owner = Owner;

        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Church(this.Owner));

        Cards.Add(new FireBall(this.Owner));
        
        Cards.Add(new Knight(this.Owner));
        Cards.Add(new FireBall(this.Owner));

        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Church(this.Owner));


        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Church(this.Owner));

        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Church(this.Owner));

        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Church(this.Owner));

        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Knight(this.Owner));
        Cards.Add(new Knight(this.Owner));

        
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));

        
        Cards.Add(new Church(this.Owner));
        Cards.Add(new Church(this.Owner));
        Cards.Add(new Church(this.Owner));
        Cards.Add(new Church(this.Owner));
        Cards.Add(new Church(this.Owner));
        Cards.Add(new Church(this.Owner));
        Cards.Add(new Church(this.Owner));
        Cards.Add(new Church(this.Owner));

        //Shuffle(Cards);
    }
}
