using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.FilePathAttribute;


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

    [SerializeField]
    private UnitDatabase AllPreFabs;

    [SerializeField]
    public TalkingText TalkingDialog;

    public int seed;
    public enum HexType {Water, Flat, Forest, High }
    public static readonly int WaterStartElevation = 20;

    private List<HexComponent> _playableHexs;
    private List<GameObject> hilightedHexs;
    //private GameObject oceanHex;

    public Level CurrentLevel;

    public enum Direction { Left, Right, UpLeft, UpRight, DownLeft, DownRight }

    private void Start()
    {
        Game.map = this;
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        GenerateMap();
        seed = Random.Range(0, 1000);


        //foreach(Player player in Game.players)
        //{
        //    player.placeAvatar();
        //}

        CurrentLevel = new LevelOne();

        CurrentLevel.StartLevel();

        _filter = Game.GetFilter();
        _playableHexs = new List<HexComponent>();

        UpdateVisable();

    }

    private void Update()
    {
        CurrentLevel.UpdateLevel();
    }

    public void PlaceItem(Construct item, Hex location)
    {

        
        GameObject itemPreFab = data.GetPrefab(item.Name);
        GameObject unitGO = Instantiate(itemPreFab, GetHexGO(location).transform.position, Quaternion.identity, GetHexGO(location).transform);
        unitGO.GetComponent<UnitComponent>().unit = (Unit)item;
        item.Location = location;
        location.cards.Add(item);
        item.Owner.AllUnits.Add(item);

        //unitGO.transform.rotation = Quaternion.Euler(0, 60 * (int)Random.Range(0, 6), 0);
        unitGO.transform.rotation = Quaternion.Euler(0, 180, 0);

        if (item.Owner == Game.players[0])
        {
            //GameObject playereffect = data.GetPrefab("PlayerAura");
            //Instantiate(playereffect, unitGO.transform.position, Quaternion.identity, unitGO.transform);
        }

        item.Pieces.Add(unitGO);
        UpdateVisable();
        //item.Owner.AllUnits.Add(item);
    }

    public GameObject PlaceEffect(string effectName, Vector3 Location)
    {
        return Instantiate(AllPreFabs.GetPrefab(effectName), Location, Quaternion.identity);
    }

    public static void RemoveGameObject(GameObject objectToDelete) 
    { 
        Destroy(objectToDelete);
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
                    var children = _hexToGameObject[highlightedHex].GetComponentsInChildren<Transform>(includeInactive: true);
                    foreach (Transform subItem in children)
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
        if (hilightedHexs == null) return;

        foreach (GameObject hex in hilightedHexs)
        {
            foreach (Transform subItem in hex.transform)
            {
                
                subItem.gameObject.layer = 0;

                if (subItem.GetComponent<UnitComponent>())
                {
                    var children = subItem.GetComponentsInChildren<Transform>(includeInactive: true);
                    foreach (Transform unitPart in children)
                    {
                        unitPart.gameObject.layer = 7;
                    }
                }
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

    public Hex GetHex(int row, int col)
    {

        return _hexes[row,col];
    }

    public Hex GetAdjacentHex(Hex hex, Direction direction)
    {
        if (hex == null) return null;
        switch (direction)
        {
            case Direction.Left:
                return GetHex(hex.row, (hex.column + 1) % this._hexes.GetLength(1));
            case Direction.Right:
                return GetHex(hex.row, (hex.column - 1) % this._hexes.GetLength(1));
            case Direction.UpLeft:
                return GetHex((hex.row + 1) % this._hexes.GetLength(0), hex.column );
            case Direction.UpRight:
                return GetHex((hex.row + 1) % this._hexes.GetLength(0), (hex.column - 1) % this._hexes.GetLength(1));
            case Direction.DownLeft:
                return GetHex((hex.row - 1) % this._hexes.GetLength(0), (hex.column + 1) % this._hexes.GetLength(1));
            case Direction.DownRight:
                return GetHex((hex.row - 1) % this._hexes.GetLength(0), hex.column);
            default:
                return null;
        }

        return null;
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
        if (hex == null) return null;
        return _hexToGameObject[hex];
    }


    public void UpdateVisable()
    {

        UnHighLightHexs();
        //Debug.Log("Updating visable rage");
        List<Hex> visableHexs = new List<Hex>();


        foreach (Construct unit in Game.players[0].AllUnits)
        {
            foreach (Hex hex in Game.map.GetHexList())
            {

                if (hex.DistanceFrom(unit.Location) < 4)
                {
                    //Debug.Log("dafd");
                    visableHexs.Add(hex);
                }
            }
        }


        Game.map.HighLightHexs(visableHexs);
        //_filter.GetComponent<Renderer>().material.color = Color.black;
    }


    public  void SelectHexs(List<Hex> hexs)
    {
        foreach (Hex hex in hexs)
        {
            hex.selected = true;
        }
    }

    public void DeSelectHexes()
    {
        foreach(Hex hex in Game.map.GetHexList())
        {
            hex.selected = false;
        }
    }
}