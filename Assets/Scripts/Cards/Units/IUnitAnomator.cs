using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitAnomator 
{



    public abstract void MoveAnimation();
    public abstract void IdleAnimation();

    public abstract void TakeDamageAnimation();


    public abstract void DeathAnimation();

    public abstract void AttackAnimation();

    public abstract void UseSpellAnimation();
}
