using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public string Name;
    public string Rules;
    public string Flavor;
    public int Priority;
    public int Range;
    public int Targets = 1;
    public int IsCard = 1;
    // Mana Cost
}