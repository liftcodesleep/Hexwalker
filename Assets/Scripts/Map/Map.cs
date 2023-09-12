using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct TileDictionary
{
    public Map.HexType type;
    public GameObject[] styles;
}

public class Map : MonoBehaviour
{
    private Hex[,] _hexes;
    private Dictionary<Hex, GameObject> _hexToGameObject;

    [SerializeField]
    private TileDictionary[] tileDictionary;

    public int seed;
    public enum HexType {Water, Flat, Forest, High }

    


    private void Start()
    {
        GenerateMap();
        seed = Random.Range(0, 1000);
    }


    public void GenerateMap()
    {
        _hexes = new Hex[Game.rows, Game.columns];
        _hexToGameObject = new Dictionary<Hex, GameObject>();

        GameObject hexGo;
        for (int column = 0; column < Game.columns; column++)
        {
            for (int row = 0; row < Game.rows; row++)
            {

                Hex h = new Hex(column, row);

                switch (h.type)
                {
                    case HexType.Water:
                        hexGo = Make_Water_Tile(h);
                        break;
                    case HexType.Flat:
                        hexGo = Make_Flat_Tile(h);
                        break;
                    case HexType.Forest:
                        hexGo = Make_Forest_Tile(h);
                        break;
                    default:
                        hexGo = Make_High_Tile(h);
                        break;
                }
                hexGo.name = h.column + ", " + h.row;

                HexComponent component = hexGo.GetComponent<HexComponent>();

                component.hex = h;

                hexGo.transform.position = component.PositionFromCamera();

                _hexes[row, column] = h;
                _hexToGameObject[h] = hexGo;
            }

        }
    }





///////////////////////////// Helper Functions ///////////////////////////////


    private GameObject Make_Water_Tile(Hex hex)
    {
        Quaternion rotation = Quaternion.Euler(0, 60 * (int)Random.Range(0, 6), 0);
        float scale = .1f;

        float x = (float)hex.column / (float)(Game.columns * scale);
        float y = (float)hex.row / (float)(Game.rows * scale);
        float noiseValue = Mathf.PerlinNoise(x, y);

        
        int subType = noiseValue > .2 ? 0 : 1;

        GameObject hexGo = (GameObject)Instantiate(
                 GetStyles(Map.HexType.Water)[subType],
                 new Vector3(0, 0, 0),
                 rotation,
                 this.transform
                 );

        return hexGo;
    }
    private GameObject Make_Flat_Tile(Hex hex)
    {
        Quaternion rotation = Quaternion.Euler(0, 60 * (int)Random.Range(0, 6), 0);
        float scale = .1f;

        float x = (float)hex.column / (float)(Game.columns * scale);
        float y = (float)hex.row / (float)(Game.rows * scale);
        float noiseValue = Mathf.PerlinNoise(x, y);

       
        int subType = noiseValue > .4 ? 0 : 1;
        GameObject hexGo = (GameObject)Instantiate(
                 GetStyles(Map.HexType.Flat)[subType],
                 new Vector3(0, 0, 0),
                 rotation,
                 this.transform
                 );

        return hexGo;
    }
    private GameObject Make_Forest_Tile(Hex hex)
    {
        Quaternion rotation = Quaternion.Euler(0, 60 * (int)Random.Range(0, 6), 0);

        float scale = .1f;

        float x = (float)hex.column / (float)(Game.columns * scale);
        float y = (float)hex.row / (float)(Game.rows * scale);
        float noiseValue = Mathf.PerlinNoise(x, y) ;

       
        int subType = noiseValue > .4 ? 0 : 1;

        GameObject hexGo = (GameObject)Instantiate(
                 GetStyles(Map.HexType.Forest)[subType],
                 new Vector3(0, 0, 0),
                 rotation,
                 this.transform
                 );

        return hexGo;
    }
    private GameObject Make_High_Tile(Hex hex)
    {
        Quaternion rotation = Quaternion.Euler(0, 60 * (int)Random.Range(0, 6), 0);
        float scale = .1f;

        float x = (float)hex.column / (float)(Game.columns * scale);
        float y = (float)hex.row / (float)(Game.rows * scale);
        float noiseValue = Mathf.PerlinNoise(x, y);

        
        int subType = noiseValue > .4 ? 0 : 1;

        GameObject hexGo = (GameObject)Instantiate(
                 GetStyles(Map.HexType.High)[subType],
                 new Vector3(0, 0, 0),
                 rotation,
                 this.transform
                 );

        return hexGo;
    }


    private GameObject[] GetStyles(Map.HexType type)
    {
        foreach(TileDictionary current_item in tileDictionary)
        {
            if (type == current_item.type)
            {
                return current_item.styles;
            }
        }
        return null;
    }

}