using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexOutLiner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject[] lines;

    [SerializeField]
    private HexComponent HexGO;

    [SerializeField]
    private GameObject HilightedObject;

    private Hex hex;

    public int offset;
    

    void Start()
    {
        hex = HexGO.hex;

        this.offset = (int)this.transform.parent.eulerAngles.y / 60 ;
        HilightedObject.SetActive (false);


    }

    // Update is called once per frame
    void Update()
    {

        
        if(hex.selected && this.offset >=0 )
        {
            HilightedObject.SetActive(true);
            if (!Game.map.GetAdjacentHex(hex, Map.Direction.UpLeft).selected)
            {
                lines[(-offset + 6 ) % 6 ].gameObject.SetActive(true);
            }
            if (!Game.map.GetAdjacentHex(hex, Map.Direction.UpRight).selected)
            {
                lines[(-offset + 7) % 6 ].gameObject.SetActive(true);
            }
            if (!Game.map.GetAdjacentHex(hex, Map.Direction.Right).selected)
            {
                lines[(-offset + 8) % 6 ].gameObject.SetActive(true);
            }
            if (!Game.map.GetAdjacentHex(hex, Map.Direction.DownRight).selected)
            {
                lines[(-offset + 9) % 6].gameObject.SetActive(true);
            }
            if (!Game.map.GetAdjacentHex(hex, Map.Direction.DownLeft).selected)
            {
                lines[(-offset + 10) % 6 ].gameObject.SetActive(true);
            }
            if (!Game.map.GetAdjacentHex(hex, Map.Direction.Left).selected)
            {
                lines[(-offset + 11) % 6].gameObject.SetActive(true);
            }
            

        }
        else if (hex.selected )
        {
            HilightedObject.SetActive(true);
            if (!Game.map.GetAdjacentHex(hex, Map.Direction.UpLeft).selected)
            {
                lines[(offset+ 0) % 6 ].gameObject.SetActive(true);
            }
            if (!Game.map.GetAdjacentHex(hex, Map.Direction.UpRight).selected)
            {
                lines[(offset + 1) % 6].gameObject.SetActive(true);
            }
            if (!Game.map.GetAdjacentHex(hex, Map.Direction.Right).selected)
            {
                lines[(offset + 2) % 6 ].gameObject.SetActive(true);
            }
            if (!Game.map.GetAdjacentHex(hex, Map.Direction.DownRight).selected)
            {
                lines[(offset + 3) % 6 ].gameObject.SetActive(true);
            }
            if (!Game.map.GetAdjacentHex(hex, Map.Direction.DownLeft).selected)
            {
                lines[(offset + 4) % 6 ].gameObject.SetActive(true);
            }
            if (!Game.map.GetAdjacentHex(hex, Map.Direction.Left).selected)
            {
                lines[(offset + 5) % 6 ].gameObject.SetActive(true);
            }


        }
        else
        {
            HilightedObject.SetActive(false);
            foreach (GameObject go in lines)
            {
                go.SetActive(false);
            }
        }
    }
}
