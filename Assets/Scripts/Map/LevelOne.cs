using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOne : Level
{
    public LevelOne() : base(1, "Tutorial", "Learn To play Hexwalker")
    {   
        AddStartingUnit(Game.players[0].Avatar, Game.map.GetHex(14, 10));
        AddStartingUnit(Game.players[1].Avatar, Game.map.GetHex(15, 35));
        AddStartingUnit(new Knight(Game.players[1]), Game.map.GetHex(18,8));

    }

    public override void StartLevel()
    {
        PlaceStartingUnits();
        //Game.players[0].Draw(2);
    }
}
