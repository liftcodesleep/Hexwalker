using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructControls : MonoBehaviour, IMouseController
{

    private UnitComponent selectedToMoveGO;

    private Unit selectedUnit;

    private GameObject _filter;
    private void Start()
    {
        _filter = Game.GetFilter();
    }

    public void close()
    {
        selectedToMoveGO = null;
        UnHighLightHexs();
        MasterMouse.taskOwner = null;
        MasterMouse.currentTask = MasterMouse.Task.StandBy;
    }

    public MasterMouse.Task GetTask()
    {
        return MasterMouse.Task.MoveUnit;
    }

    public void LeftClicked(GameObject clickObject)
    {
        Debug.Log("CC LC");
        
        selectedToMoveGO = clickObject.GetComponent<UnitComponent>();

        if(!selectedToMoveGO)
        {
            Debug.Log("Construct Controlls: Not unit clicked");
            return;
        }
        
        selectedUnit = selectedToMoveGO.unit;
        
        if (!selectedToMoveGO)
        {
            Debug.Log("Unit was Construct was null In left click");
            throw new NullReferenceException("Unit was Construct was null In left click");
        }

        HighLightHexs();
    }

    public void open()
    {
        Debug.Log("CC open");


        switch (MasterMouse.currentTask)
        {

            case MasterMouse.Task.PlayCard:
            case MasterMouse.Task.StandBy:
            case MasterMouse.Task.Transition:
                MasterMouse.taskOwner.close();
                MasterMouse.currentTask = GetTask();
                MasterMouse.taskOwner = this;
                break;

            default:
                //MasterMouse.currentTask = MasterMouse.Task.Transition;
                break;

        }

    }

    public void RightClicked(GameObject clickObject)
    {

        HexComponent targetHexGO = clickObject.GetComponent<HexComponent>();

        if(!targetHexGO)
        {
            return;
        }
        Debug.Log("CC Moving From " + selectedUnit.Location.Name);
        int hexesMoved  = this.selectedUnit.Move(targetHexGO.hex);
        Debug.Log("CC Moving to " + selectedUnit.Location.Name);

        close();

    }




    private void HighLightHexs()
    {
        UnHighLightHexs();
        GameObject hexMap = Game.GetHexMapGo();

        _filter.SetActive(true);
        HexComponent hexGO;
        foreach (Transform currentHex in hexMap.transform)
        {

            hexGO = currentHex.gameObject.GetComponent<HexComponent>();

            //selectedUnit.Location.DistanceFrom(hexGO.hex) < selectedUnit.ActionPoints &&

            if (selectedUnit.Location.DistanceFrom(hexGO.hex) <= selectedUnit.ActionPoints && selectedUnit.ValidMove(hexGO.hex))
            {

                hexGO.gameObject.layer = 6;
                

                foreach (Transform subItem in hexGO.transform)
                {
                    subItem.gameObject.layer = 6;
                }


            }
            else
            {
                foreach (Transform subItem in hexGO.transform)
                {
                    subItem.gameObject.layer = 0;
                }

            }

            

        }

        
        foreach (Transform subItem in  Game.map.GetHexGO( selectedUnit.Location).transform )
        {
            subItem.gameObject.layer = 6;
        }


    }

    private void UnHighLightHexs()
    {

        Debug.Log(" CC Turning off highlight");
        _filter.SetActive(false);
        GameObject hexMap = Game.GetHexMapGo();
        HexComponent hexGO;
        foreach (Transform currentHex in hexMap.transform)
        {
            hexGO = currentHex.gameObject.GetComponent<HexComponent>();
            foreach (Transform subItem in hexGO.transform)
            {
                subItem.gameObject.layer = 0;
            }
        }
    }
}
