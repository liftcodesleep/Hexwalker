using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : Card
{

  

    public Spell(Player owner) : base(owner) {
        type = Card.Type.SPELL;
        
    }
    
}
