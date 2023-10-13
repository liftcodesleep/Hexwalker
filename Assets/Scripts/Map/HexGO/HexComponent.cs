using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HexComponent : MonoBehaviour
{
    public Hex hex;


    private static readonly float RADIUS = .5774f;
    private static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2f;
    private static readonly float HEIGHT = RADIUS * 2f;
    private static readonly float WIDTH = WIDTH_MULTIPLIER * HEIGHT;
    private static readonly float HexVerticalSpacing = HEIGHT * 0.75f;
    private static readonly float HexHorizontalSpacing = WIDTH;


    private void Start()
    {
        this.transform.localScale = new Vector3(.01f, .01f, .01f);
    }

    private void Update()
    {
        if (this.transform.localScale.x < 1)
        {
            this.transform.localScale += new Vector3(.02f, .02f, .02f);
        }
    }

    public Vector3 Position()
    {
        float z;

        z = this.transform.parent.transform.position.z;
        //z = Game.map.transform.position.z;
       
        return new Vector3(
         HexHorizontalSpacing * (hex.sum + hex.row / 2f),
         0,
         z + HexVerticalSpacing * hex.row
        );

    }

    public void UpdatePosition()
    {
        this.transform.position = new Vector3(
            this.PositionFromCamera().x,
            this.transform.position.y,
            this.transform.position.z);
    }

    
    public Vector3 PositionFromCamera(Vector3 cameraPosition)
    {

        
        float mapWidth = Game.columns * HexHorizontalSpacing;

        Vector3 position = this.Position();

        float howManyWidthsFromCamera = (position.x - cameraPosition.x) / mapWidth;


        //We want howmanyWidthsFromCamera to be between -0.5 to 0.5
        if (howManyWidthsFromCamera > 0)
        {
            howManyWidthsFromCamera += 0.5f;
        }
        else
        {
            howManyWidthsFromCamera -= 0.5f;
        }

        int howManyWidthToFix = (int)howManyWidthsFromCamera;


        position.x -= howManyWidthToFix * mapWidth;

  
        return position;

    }


    public Vector3 PositionFromCamera()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        
        return PositionFromCamera(cameraPosition);
    }

 
}