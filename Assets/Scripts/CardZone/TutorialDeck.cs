using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDeck : CardZone
{
    
    //public KnightsDeck(Player Owner)
    //{
    //    this.Owner = Owner;
    //}

    public TutorialDeck(Player Owner)
    {
        this.Owner = Owner;
        //Cards.Add(new Archer(this.Owner));
        Cards.Add(new ConnectToNature(this.Owner));
        //Cards.Add(new Knight(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new GiantGrowth(this.Owner));
        Cards.Add(new ConnectToNature(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));
        Cards.Add(new Bear(this.Owner));



        //Cards.Add(new Bear(this.Owner));


        //.Add(new FireBall(this.Owner));

        //Cards.Add(new Knight(this.Owner));


        //Shuffle(Cards);
    }
}
