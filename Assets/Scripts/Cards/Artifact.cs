using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Artifact : Construct
{
    public int HealthPoints;
    public int Health;

    public Artifact(Player Owner) : base(Owner) { }
}
