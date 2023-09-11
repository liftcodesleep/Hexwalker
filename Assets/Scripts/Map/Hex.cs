using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// The Hex class defines the grid position, world space position, size,
/// neighbours,etc.. of a Hex Tile. However, it doesn NOT interact with
/// Unity directly in any way.
/// </summary>


public class Hex
{
    //private readonly string name = "Hex.Name";
    // private HashSet<Unit> units;
    private int seed;
    public readonly int column;
    public readonly int row;
    public readonly int sum;
    //TODO determine job of elevation
    public int elevation;

    public Hex(int q, int r)
    {
        this.column = q;
        this.row = r;
        this.sum = -(q + r);
        this.seed = CalcuateStyle();
       // this.Units = new HashSet<Unit>();
    }

    public int CalcuateStyle()
    {
        System.Random rand = new System.Random();
        //float smoothness = 1;
        //float scale = 10;
        //int LocalSeed = (int)(Mathf.PerlinNoise(((column * smoothness) + HexDimensions.GetOffset() + (int)(row / 2)) / scale, ((row * smoothness) + HexDimensions.GetOffset()) / scale) * 100);
        
        //elevation = (int)(Mathf.PerlinNoise(((column * smoothness) + HexDimensions.GetOffset() + (int)(row / 2)) / scale, ((row * smoothness) + HexDimensions.GetOffset()) / scale) * 100);

        if (elevation > 75)
        {

            return 4;
        }
        else if (elevation > 53)
        {
            if (rand.Next(0, 20) == 1)
            {
                return 0;
            }

            if (rand.Next(0, 10) == 1)
            {
                return 1;
            }
            return 3;
        }
        else if (elevation > 38)
        {
            if (rand.Next(0, 20) == 1)
            {
                return 0;
            }
            if (rand.Next(0, 30) == 1)
            {
                return 4;
            }
            return 2;
        }
        else if (elevation > 28)
        {
            if (rand.Next(0, 30) == 1)
            {
                return 4;
            }
            return 1;
        }
        else
        {
            if (rand.Next(0, 30) == 1)
            {
                return 4;
            }
            return 0;
        }
    }
    
//    public void AddUnit(Unit unit)
//    {
//        Units.Add(unit);
//    }
//
//    public void RemoveUnit(Unit unit)
//    {
//        if (Units.Count != 0)
//        {
//            Units.Remove(unit);
//        }
//    }

//    public Unit[] GetUnitsArray()
//    {
//        return Units.ToArray();
//    }
}