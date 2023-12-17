using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.FilePathAttribute;


////////////////For Organazation //////////////////////////////////////

[System.Serializable]
public struct TileStyle {
    public int minElevation;
    public int maxElevation;
    public int chanceOfSpawning;
    public GameObject style;
}

[System.Serializable]
public struct TileDictionary {
    public Map.HexType type;
    public TileStyle[] styles;
}
//////////////////////////////////////////////////////




public class Map : MonoBehaviour {
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
    public int columns;
    public int rows;
    public enum HexType {Water, Flat, Forest, High }
    public static readonly int WaterStartElevation = 20;
    private List<HexComponent> _playableHexs;
    private List<GameObject> highlightedHexes;
    //private GameObject oceanHex;
    public Level CurrentLevel;
    public enum Direction { Left, Right, UpLeft, UpRight, DownLeft, DownRight }

    private void Start() {
        Game.map = this;
        while (transform.childCount > 0) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        this.rows = 28;
        this.columns = 36;
        GenerateMap();
        seed = Random.Range(0, 1000);
        //foreach(Player player in Game.players)
        //{
        //    player.placeAvatar();
        //}
        CurrentLevel = new TestLevel();
        CurrentLevel.StartLevel();
        _filter = Game.GetFilter();
        _playableHexs = new List<HexComponent>();
        UpdateVisible();
    }

    private void Update() {
        CurrentLevel.UpdateLevel();
    }

    public void PlaceItem(Construct item, Hex location) {
        GameObject itemPreFab = data.GetPrefab(item.Name);
        GameObject unitGO = Instantiate(itemPreFab, GetHexGO(location).transform.position, Quaternion.identity, GetHexGO(location).transform);
        unitGO.GetComponent<UnitComponent>().unit = (Unit)item;
        item.Location = location;
        location.Constructs.Add(item);
        item.Owner.Units.Add(item);

        //unitGO.transform.rotation = Quaternion.Euler(0, 60 * (int)Random.Range(0, 6), 0);
        unitGO.transform.rotation = Quaternion.Euler(0, 180, 0);

        if (item.Owner == Game.players[0])
        {
            //GameObject playereffect = data.GetPrefab("PlayerAura");
            //Instantiate(playereffect, unitGO.transform.position, Quaternion.identity, unitGO.transform);
        }

        item.Pieces.Add(unitGO);
        UpdateVisible();
        //item.Owner.Units.Add(item);
    }

    public GameObject PlaceEffect(string effectName, Vector3 Location) {
        return Instantiate(AllPreFabs.GetPrefab(effectName), Location, Quaternion.identity);
    }

    public static void RemoveGameObject(GameObject objectToDelete) {
        Destroy(objectToDelete);
    }

    public Hex GetRandomHex() {
        return _hexes[Random.Range(0, this.rows-1), Random.Range(0, this.columns-1)];
    }

    public void GenerateMap() {
        _hexes = new Hex[this.rows, this.columns];
        _hexToGameObject = new Dictionary<Hex, GameObject>();
        GameObject hexGo;
        for (int column = 0; column < this.columns; column++) {
            for (int row = 0; row < this.rows; row++) {
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

    public void HighlightHexes(List<Hex> hexesToHighlight) {
        _filter.SetActive(true);
        highlightedHexes = new List<GameObject>();
        foreach (Hex highlightedHex in hexesToHighlight) {
            foreach(Hex currentMapHex in _hexes) {
                if(highlightedHex == currentMapHex) {
                    var children = _hexToGameObject[highlightedHex].GetComponentsInChildren<Transform>(includeInactive: true);
                    foreach (Transform subItem in children) {
                        subItem.gameObject.layer = 6;
                    }
                    highlightedHexes.Add(_hexToGameObject[highlightedHex]);
                    break;
                }
            }
        }
    }

    public void UnhighlightHexes() {
        _filter.SetActive(false);
        if (highlightedHexes == null) return;
        foreach (GameObject hex in highlightedHexes) {
            foreach (Transform subItem in hex.transform) {
                subItem.gameObject.layer = 0;
                if (subItem.GetComponent<UnitComponent>()) {
                    var children = subItem.GetComponentsInChildren<Transform>(includeInactive: true);
                    foreach (Transform unitPart in children) {
                        unitPart.gameObject.layer = 7;
                    }
                }
            }
        }
    }

    public List<Hex> GetHexList() {
        List<Hex> hexList = new List<Hex>();
        foreach(Hex hex in _hexes) {
            hexList.Add(hex);
        }
        return hexList;
    }

    public Hex GetHex(int row, int col) {
        return _hexes[row,col];
    }

    public Hex GetAdjacentHex(Hex hex, Direction direction) {
        if (hex == null)  {
            return null;
        }
        switch (direction) {
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
    }


    ///////////////////////////// Helper Functions ///////////////////////////////


    private GameObject MakeTile(Hex hex) {
        List<TileStyle> styles = GetStyles(hex);
        int index = Random.Range(0, styles.Count);
        Quaternion rotation = Quaternion.Euler(0, 60 * (int)Random.Range(0, 6), 0);
        GameObject hexGo = Instantiate(
                 styles[index].style,
                 new Vector3(0, 0, 0),
                 rotation,
                 this.transform
                 );
        return hexGo;
    }

    private List<TileStyle> GetStyles(Hex hex) {
        List<TileStyle> styles = new List<TileStyle>();
        foreach(TileDictionary styleArray in tileDictionary) {
            if (hex.type == styleArray.type) {
                foreach(TileStyle style in styleArray.styles) {
                    if(style.minElevation < hex.elevation && style.maxElevation > hex.elevation) {
                        for(int i = 0; i < style.chanceOfSpawning; i++) {
                            styles.Add(style);
                        }
                    }
                }
                return styles;
            }
        }
        return null;
    }


    public GameObject GetHexGO(Hex hex) {
        if (hex == null) return null;
        return _hexToGameObject[hex];
    }


    public void UpdateVisible() {
        UnhighlightHexes();
        //Debug.Log("Updating visable rage");
        List<Hex> visibleHexes = new List<Hex>();
        foreach (Construct unit in Game.players[0].Units) {
            foreach (Hex hex in Game.map.GetHexList()) {
                if (hex.DistanceFrom(unit.Location) < 4) {
                    //Debug.Log("dafd");
                    visibleHexes.Add(hex);
                }
            }
        }
        Game.map.HighlightHexes(visibleHexes);
        //_filter.GetComponent<Renderer>().material.color = Color.black;
    }


    public  void SelectHexs(List<Hex> hexs){
        foreach (Hex hex in hexs){
            hex.selected = true;
        }
    }

    public void DeselectHexes() {
        foreach(Hex hex in Game.map.GetHexList()) {
            hex.selected = false;
        }
    }
}
