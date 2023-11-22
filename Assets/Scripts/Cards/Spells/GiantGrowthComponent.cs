using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantGrowthComponent : MonoBehaviour
{

    UnitComponent unit;
    int startTurn;

    Vector3 originalSize;
    float growth_scale = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        unit = this.transform.parent.GetComponentInChildren<UnitComponent>();

        startTurn = Game.turnCount;
        this.originalSize = unit.transform.localScale;

        StartCoroutine(Grow());
        //unit.transform.localScale *= 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(startTurn != Game.turnCount)
        {
            unit.transform.localScale = originalSize;
            Destroy(this);
        }

    }

    IEnumerator Grow()
    {
        while(unit.transform.localScale.magnitude < (originalSize * growth_scale).magnitude)
        {
            unit.transform.localScale *= 1.1f;
            yield return new WaitForSeconds(.1f);
        }
        
    }
}
