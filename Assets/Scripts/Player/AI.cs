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


        //Debug.Log("AI is at " + this.Avatar.Location.row + ", " + this.Avatar.Location.column);

        //Game.map.StartCoroutine(HandleTurn());
        MoveUnitToLocation((Unit)Game.players[1].AllUnits[2], Game.players[0].Avatar.Location);
        Debug.Log("Moving " + Game.players[1].AllUnits[2].Location.column + " " + Game.players[1].AllUnits[2].Location.row);
        Game.NextTurn();
    }

    public override void OnTurnEnd()
    {

    }


    IEnumerator HandleTurn()
    {
        MoveUnitToLocation(this.Avatar, Game.players[0].Avatar.Location);

        //Game.map.StartCoroutine(Move ());
        //Move();
        Debug.Log("Finished Move AI");
        //yield return new WaitForSeconds(2f);
        ////PlayCard.Play(this.Hand.Cards[0], Game.map.GetAdjacentHex(this.Avatar.Location, Map.Direction.UpRight));
        //Debug.Log("Finished Plaing  AI");
        yield return new WaitForSeconds(2f);
        Debug.Log("AI ended turn");
        Game.NextTurn();
        
    }

    IEnumerator Move()
    {

        List<Hex> path = FindPath(this.Avatar, Game.players[0].Avatar.Location, new List<Hex>());

        Debug.Log("The found path size was " + path.Count);

        foreach (Hex hex in path)
        {
            yield return new WaitForSeconds(1f);
            this.Avatar.Move(hex);
        }
        
           
    
    }


    /*
     * Just moves closer to the target. Does not find true path
     */
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

        //Debug.Log("AAAAAA0" + currentPath.Count);
        //List<Hex> newPath = new List<Hex>(currentPath);
        //
        Debug.Log("AAAAAA " + currentPath.Count );


        if(currentPath.Count > 0 && currentPath.Last() == destantion)
        {
            Debug.Log("Got the path!! its size is " + currentPath.Count);
            return currentPath;
        }

        /*
         * Tests the path for every hex next to the current hex
         * Do another recursive call if the hex that it was nex to was not already in the list
         */
        Hex newLocation;
        List<Hex> newPath;
        List<Hex>[] allPaths = new List<Hex>[6];
        for (int direction_index = 0; direction_index < 6; direction_index++)
        {
            newLocation = Game.map.GetAdjacentHex(unit.Location, (Map.Direction)direction_index);

            if(currentPath.Contains(newLocation))
            {
                continue;
            }


            newPath = new List<Hex>(currentPath);
            newPath.Add(newLocation);

            allPaths[direction_index] =  FindPath(unit, destantion, newPath);


        }

        newPath = allPaths[0];
        foreach (List<Hex> path in allPaths)
        {
            if(path != null && path.Count < newPath.Count) 
            {
                newPath = path;
            }
        }
        return newPath;

    }
}
