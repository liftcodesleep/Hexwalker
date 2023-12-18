using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Construct : Card {
    public Card card;
    //public List<Effect> ConstructEffects;
    //    public abstract int Spawn();
    //    public abstract int Despawn();
    //    public abstract int ActivateAbility();

    public List<Effect> ActiveEffects;
    public List<Effect> Abilities;

    public Construct(Player owner) : base(owner) {
        ActiveEffects = new List<Effect>();
        Abilities = new List<Effect>();
        //owner.Units.Add(this);
    }
}
