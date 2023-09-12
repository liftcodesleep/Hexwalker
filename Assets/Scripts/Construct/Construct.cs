using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Construct : Card
{
    public Card Card;
    public Hex Location;

    public abstract int Spawn();
    public abstract int Despawn();
}