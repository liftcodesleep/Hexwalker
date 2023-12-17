using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AStartPathfinding : MonoBehaviour
{
    private class AStarData
    {
        public Hex hex;
        public float GCost; //Distance from starting node
        public float HCost; // Distance from end node
        public float FCost; // G+H
        public AStarData hexParentInClosed; // The hex that it last came from
    }


    public static List<Hex> AStartPath(Unit unit, Hex starting, Hex ending)
    {
        //Debug.Log("Setup");
        List<AStarData> open = new List<AStarData>();
        List<AStarData> closed = new List<AStarData>();

        open.Add(new AStarData
        {
            hex = starting,
            GCost = 0,
            HCost = starting.DistanceFrom(ending),
            FCost = starting.DistanceFrom(ending),
            hexParentInClosed = null
        }); ;

        
        //Debug.Log("starting up");
        AStarData current;
        while (true)
        {
            
            current = getLowestCost(open);
            //Debug.Log("Inside: " + current.hex.row + " " + current.hex.column);
            open.Remove(current);
            closed.Add(current);
            

            if (current.hex == ending) return getFinalPath(current, closed);


            // Updates the neighboring hexs
            foreach(Hex currentNeighbour in GetNeighbors(current.hex))
            {

                if( ! unit.moveableHexTypes.Contains(currentNeighbour.type) || GetHexInData(currentNeighbour, closed).hex != null )
                {
                    continue;
                }

                // Updates all neighouring hexs
                AStarData found = GetHexInData(currentNeighbour, open);
                if (found.hex != null &&  found.FCost > current.FCost + 1  ) /// UPDATE +1 this is the move cost to move to the hex
                {
                    found.FCost = current.FCost + 1;
                }
                else
                {
                    open.Add(new AStarData
                    {
                        hex = currentNeighbour,
                        GCost = current.GCost + 1,
                        HCost = starting.DistanceFrom(ending),
                        FCost = starting.DistanceFrom(ending) + current.GCost + 1,
                        hexParentInClosed = current
                    }); 
                }

            }


        }



        //return getFinalPath(current, closed);


    }

    


    private static AStarData GetHexInData(Hex hex, List<AStarData> data)
    {

        foreach(AStarData current in data)
        {
            if(current.hex == hex ) return current;
        }
        return new AStarData { hex = null};
    } 


    private static AStarData getLowestCost(List<AStarData> data)
    {
        AStarData lowest = data[0];
        foreach (AStarData current in data)
        {
            if(current.FCost < lowest.FCost)
            {
                lowest = current;
            }
        }
        return lowest;
    }

    private static List<Hex> getFinalPath(AStarData data, List<AStarData> closedData)
    {
        List<Hex> path = new List<Hex>();
        
        AStarData currentData = data;
        path.Add(currentData.hex);

        while (currentData.hexParentInClosed != null)
        {
            currentData = currentData.hexParentInClosed;
            path.Add(currentData.hex);
            
        }

        path.Reverse();

        return path;


    }


    


    private static List<Hex> GetNeighbors(Hex hex)
    {
        List<Hex> neighbors = new List<Hex>();
        Hex neighbor;
        for (int dir = 0; dir < 6; dir++)
        {
            neighbor = Game.map.GetAdjacentHex(hex, (Map.Direction)dir);
            if (neighbor != null)
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }
}
