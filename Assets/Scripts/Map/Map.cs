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

    [SerializeField]
    UnitDatabase data;

    [SerializeField]
    private GameObject _filter;

    

    public int seed;
    public enum HexType {Water, Flat, Forest, High }
    public static readonly int WaterStartElevation = 20;

    private List<HexComponent> _playableHexs;
    private List<GameObject> hilightedHexs;
    //private GameObject oceanHex;

    private void Start()
    {
        Game.map = this;
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        GenerateMap();
        seed = Random.Range(0, 1000);


        foreach(Player player in Game.players)
        {
            player.placeAvatar();
        }

        _filter = Game.GetFilter();
        _playableHexs = new List<HexComponent>();

    }

    public void PlaceItem(Construct item, Hex location)
    {

        
        GameObject itemPreFab = data.GetPrefab(item.Name);
        GameObject unitGO = Instantiate(itemPreFab, GetHexGO(location).transform.position, Quaternion.identity, GetHexGO(location).transform);
        unitGO.GetComponent<UnitComponent>().unit = (Unit)item;
        item.Location = location;
        location.cards.Add(item);

        unitGO.transform.rotation = Quaternion.Euler(0, 60 * (int)Random.Range(0, 6), 0);

        if (item.Owner == Game.players[0])
        {
            //GameObject playereffect = data.GetPrefab("PlayerAura");
            //Instantiate(playereffect, unitGO.transform.position, Quaternion.identity, unitGO.transform);
        }

        item.Pieces.Add(unitGO);
    }

    public Hex GetRandomHex()
    {
        return _hexes[Random.Range(0, Game.rows-1), Random.Range(0, Game.columns-1)];
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

                
            }

        }
    }


    public void HighLightHexs(List<Hex> hexsToHighlight)
    {
        
        _filter.SetActive(true);
        hilightedHexs = new List<GameObject>();


        foreach (Hex highlightedHex in hexsToHighlight)
        {
            foreach(Hex currentMapHex in _hexes)
            {
                if(highlightedHex == currentMapHex)
                {
                    foreach (Transform subItem in _hexToGameObject[highlightedHex].transform)
                    {
                        subItem.gameObject.layer = 6;
                    }
                    hilightedHexs.Add(_hexToGameObject[highlightedHex]);
                    break;
                }
                
            }
        }

        
    }

    public void UnHighLightHexs()
    {
        _filter.SetActive(false);
        foreach (GameObject hex in hilightedHexs)
        {
            foreach (Transform subItem in hex.transform)
            {
                subItem.gameObject.layer = 0;
            }
        }

    }

    public List<Hex> GetHexList()
    {
        List<Hex> hexList = new List<Hex>();

        foreach(Hex hex in _hexes)
        {
            hexList.Add(hex);
        }

        return hexList;
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


    public GameObject GetHexGO(Hex hex)
    {
        return _hexToGameObject[hex];
    }
}