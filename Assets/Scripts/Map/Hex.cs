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

    public string Name { get; set; }
    public readonly Map.HexType type;
    // private HashSet<Unit> units;
    
    public readonly int column;
    public readonly int row;
    public readonly int sum;

    //TODO determine job of elevation
    public int elevation;

    public List<Card> cards;

    public List<Effect> ActiveEffects;

    public Hex(int q, int r)
    {
        this.column = q;
        this.row = r;
        this.sum = -(q + r);

        this.type = CalcuateType();

        cards = new List<Card>();
        ActiveEffects = new List<Effect>();
        //TODO:
        // this.Units = new HashSet<Unit>();
    }

    private Map.HexType CalcuateType()
    {

        this.elevation = CalculateElevation();

        this.elevation += CalculateIslandElevationOffset();

        if (elevation < 40)
        {
            Name = "Water";
            return Map.HexType.Water;
        }else if (elevation < 52)
        {
            Name = "Flat";
            return Map.HexType.Flat;
        }
        else if (elevation < 63)
        {
            Name = "Forest";
            return Map.HexType.Forest;
        }
        Name = "High";
        return Map.HexType.High;
    }




    ///////////////////////////////////////// HELPER FUNCTIONS ////////////////////////////////////

    private int CalculateElevation()
    {
        float x, y, noiseValue, elevation;
        float scale = .55f;

        // First layer of noise
        x = (float)column / (float)(Game.columns * scale);
        y = (float)row / (float)(Game.rows * scale);
        noiseValue = Mathf.PerlinNoise(x, y) * 100f;
        elevation = (int)noiseValue;

        // Second layer of noise (more chaotic)
        scale = .1f;
        x = (float)column / (float)(Game.columns * scale);
        y = (float)row / (float)(Game.rows * scale);
        noiseValue = Mathf.PerlinNoise(x, y) + .5f;

        // Only change the largest values
        noiseValue = Mathf.Pow(noiseValue, 2) * 20 - 24;
        if (Mathf.Abs(noiseValue) > 10)
        {
            elevation += (int)noiseValue;
        }

        return (int)elevation;

    }


    private int CalculateIslandElevationOffset()
    {
        int from_center = (Game.rows-1) / 2 - row;

        float island_steapness = 30f;
        float island_width = 70;
        float island_hight_increase = 50;
        // Bell like curve
        float island_offset = Mathf.Pow(2, -Mathf.Pow(from_center, 2) / island_steapness) * island_width - island_hight_increase;

        if (island_offset > 0)
        {
            island_offset = 0;
        }

        return (int)island_offset;
    }


    public float DistanceFrom(Hex b)
    {

        int dQ = Mathf.Abs(this.column - b.column);

        if (dQ > Game.columns / 2)
        {
            dQ = Game.columns - dQ;
        }

        int dS = Mathf.Abs(this.sum - b.sum);
        if (dS > Game.columns / 2)
        {
            dS = Mathf.Abs(dS - Game.columns);
        }

        return Mathf.Max(
            dQ,
            Mathf.Abs(this.row - b.row),
            dS
            );
    }


}