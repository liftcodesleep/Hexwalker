using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanCreation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetUpOcean());
    }

 

    IEnumerator SetUpOcean()
    {
        

        yield return new WaitForSeconds(1);

        transform.SetParent(Game.map.gameObject.transform.GetChild(0).transform);

        

        //this.gameObject.se //=  Game.map.gameObject.transform.GetChild(0).transform;

    }
}
