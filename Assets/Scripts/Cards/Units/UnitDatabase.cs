using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class UnitDatabase : MonoBehaviour
{
    [System.Serializable]
    public struct UnitMap
    {

        public string name;
        public GameObject prefab;
    }



    [SerializeField]
    public List<UnitMap> units;


    public GameObject GetPrefab(string name)
    {

        foreach (UnitMap unit in units)
        {
            if(name.Equals(unit.name))
            {
                
                return unit.prefab;
            }

            //Debug.Log(unit.name);
        }

        return null;
    }


    public void PlaceGameObject(string name, Hex location)
    {

    }


}
