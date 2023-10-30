using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AI : Player
{

    public AI()
    {

        this.Deck = new AIDeck(this);

    }

    public override void OnTurnStart()
    {
        Draw(1);


        //Hex startingLocation = this.Avatar.Location;
        ////this.Avatar.Move(  Game.map.GetAdjacentHex( this.Avatar.Location, Map.Direction.DownRight  )  );
        //
        //while  (startingLocation == this.Avatar.Location)
        //{
        //    this.Avatar.Move(Game.map.GetAdjacentHex(this.Avatar.Location, (Map.Direction)Random.Range(0, 5) )); 
        //}

        //MoveUnitToLocation(this.Avatar, Game.players[0].Avatar.Location);

        Debug.Log("AI is at " + this.Avatar.Location.row + ", " + this.Avatar.Location.column);

        

        Game.map.StartCoroutine(HandleTurn());

        //this.Hand.Cards[0];
        
        //Game.NextTurn();
    }

    public override void OnTurnEnd()
    {

    }


    IEnumerator HandleTurn()
    {
        MoveUnitToLocation(this.Avatar, Game.players[0].Avatar.Location);
        yield return new WaitForSeconds(1f);
        PlayCard.Play(this.Hand.Cards[0], Game.map.GetAdjacentHex(this.Avatar.Location, Map.Direction.UpRight));
        yield return new WaitForSeconds(1f);
        Game.NextTurn();
    }

    IEnumerator Move()
    {
        
        yield return new WaitForSeconds(.005f);
           

    }



    public void MoveUnitToLocation(Unit unit, Hex target_location)
    {
        Hex startingLocation = unit.Location;

        for (int direction_index = 0; direction_index < 6; direction_index++)
        {
            Hex newLocation = Game.map.GetAdjacentHex(unit.Location, (Map.Direction)direction_index);

            if (newLocation.DistanceFrom(target_location) < startingLocation.DistanceFrom(target_location))
            {
                
                if(unit.ValidMove(newLocation))
                {
                    unit.Move(newLocation);
                    return;
                }
            }

        }

        Debug.Log("!!!!!!!!!! AI Cound not find a good path");
        while (startingLocation == this.Avatar.Location)
        {
            this.Avatar.Move(Game.map.GetAdjacentHex(this.Avatar.Location, (Map.Direction)Random.Range(0, 5)));
        }

    }


    public List<Hex> FindPath(Unit unit, Hex destantion, List<Hex> currentPath)
    {
        List<Hex> newPath = new List<Hex>(currentPath);

        if(currentPath.Last() == destantion)
        {
            return currentPath;
        }

        for (int direction_index = 0; direction_index < 6; direction_index++)
        {
            Hex newLocation = Game.map.GetAdjacentHex(unit.Location, (Map.Direction)direction_index);

            //if (newLocation.DistanceFrom(target_location) < startingLocation.DistanceFrom(target_location))
            //{
            //    unit.Move(newLocation);
            //    if (unit.ValidMove(newLocation))
            //    {
            //        return;
            //    }
            //}


        }

        return null;

    }
}
