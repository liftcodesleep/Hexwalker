using System.Collections;
using System.Collections.Generic;
using UnityEngine;


////////////////For Organazation //////////////////////////////////////

[System.Serializable]
public struct TileStyle
{
    public int minElevation;
    public int maxElevation;
    public int chanceOfSpawning;
    public GameObject style;
}

[System.Serializable]
public struct TileDictionary
{
    public Map.HexType type;
    public TileStyle[] styles;
}
//////////////////////////////////////////////////////


public class Map : MonoBehaviour
{
    private Hex[,] _hexes;
    private Dictionary<Hex, GameObject> _hexToGameObject;

    [SerializeField]
    private GameObject ocean;

    [SerializeField]
    private TileDictionary[] tileDictionary;

    public int seed;
    public enum HexType {Water, Flat, Forest, High }
    public static readonly int WaterStartElevation = 20;
    


    private void Start()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
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
                hexGo = MakeTile(h);
                
                hexGo.name = h.column + ", " + h.row;

                HexComponent component = hexGo.GetComponent<HexComponent>();

                component.hex = h;

                hexGo.transform.position = component.PositionFromCamera();

                _hexes[row, column] = h;
                _hexToGameObject[h] = hexGo;

                if(row == Game.rows/2 && column == Game.columns/2)
                {
                    GameObject heoceaGo = Instantiate(
                     ocean,
                     new Vector3(0, 0, 0),
                     Quaternion.identity,
                     hexGo.transform
                     );

                    hexGo.name += " Ocean";
                }
            }

        }
    }





///////////////////////////// Helper Functions ///////////////////////////////


    private GameObject MakeTile(Hex hex)
    {
        List<TileStyle> posibleStyles = GetStyles(hex);

        int index = Random.Range(0, posibleStyles.Count);

        Quaternion rotation = Quaternion.Euler(0, 60 * (int)Random.Range(0, 6), 0);
        GameObject hexGo = Instantiate(
                 posibleStyles[index].style,
                 new Vector3(0, 0, 0),
                 rotation,
                 this.transform
                 );
        return hexGo;
        
    }

    private List<TileStyle> GetStyles(Hex hex)
    {
        List<TileStyle> posibleStyles = new List<TileStyle>();

        foreach(TileDictionary styleArray in tileDictionary)
        {
            if (hex.type == styleArray.type)
            {
                foreach(TileStyle style in styleArray.styles)
                {

                    if(style.minElevation < hex.elevation && style.maxElevation > hex.elevation)
                    {
                        for(int i = 0; i < style.chanceOfSpawning; i++)
                        {
                            posibleStyles.Add(style);
                        }
                    }
                }

                return posibleStyles;


            }
        }
        return null;
    }

}