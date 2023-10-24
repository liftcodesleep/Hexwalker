using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Player
{

    public AI()
    {

    }

    public override void OnTurnStart()
    {
        Draw(1);
        Debug.Log("AI Does nothing");
        
        Game.NextTurn();
    }

    public override void OnTurnEnd()
    {

    }


    IEnumerator Move()
    {
        
        yield return new WaitForSeconds(.005f);
           

    }
}
