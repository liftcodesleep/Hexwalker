using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public abstract class Level
{

    public struct Construct_and_Location
    {
        public Construct construct;
        public Hex location;
    }

    public int level;
    public string name;
    public string description;
    public List<Construct_and_Location> starting_constructs;
    

    public Level(int level, string name, string description) {
        this.level = level;
        this.name = name;
        this.description = description;

        starting_constructs = new List<Construct_and_Location>();
    }

    public void AddStartingUnit(Construct unit, Hex Location) {
        starting_constructs.Add(new Construct_and_Location() { construct = unit, location = Location });
        
    }

    public void PlaceStartingUnits() {
        foreach( Construct_and_Location unit in starting_constructs ) {
            Game.map.PlaceItem(unit.construct, unit.location);

        }
    }

    public abstract void StartLevel();
    public abstract void OnStartTurn(int turn);
    public abstract void UpdateLevel();

}
