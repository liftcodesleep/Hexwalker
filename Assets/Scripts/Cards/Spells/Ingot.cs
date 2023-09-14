using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingot : Artifact
{
    public Ingot()
    {
        Cost = new Charge(0, 1, 0, 0, false);
        Health = 4;
    }
}