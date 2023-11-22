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
        //Debug.Log("CC LC");
        
        selectedToMoveGO = clickObject.GetComponent<UnitComponent>();

        if(!selectedToMoveGO)
        {
            //Debug.Log("Construct Controlls: Not unit clicked");
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
        //Debug.Log("CC open");


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

        if(targetHexGO)
        {
            move_to(targetHexGO);
            return;
        }

        UnitComponent targetUnitGO = clickObject.GetComponent<UnitComponent>();
        if (targetUnitGO)
        {

            targetUnitGO.GetComponentInChildren<Knight_Animation_Controller>().TakeDamageAnimation();
            selectedToMoveGO.GetComponentInChildren<Knight_Animation_Controller>().AttackAnimation();

            targetUnitGO.transform.LookAt(selectedToMoveGO.transform.position);
            selectedToMoveGO.transform.LookAt(targetUnitGO.transform.position);

            selectedUnit.Attack(targetUnitGO.unit);
            close();
        }

    }

    public static void PlayAttackAnimations(Unit attacker, Unit defender)
    {

        defender.Pieces[0].GetComponentInChildren<Knight_Animation_Controller>().TakeDamageAnimation();
        attacker.Pieces[0].GetComponentInChildren<Knight_Animation_Controller>().AttackAnimation();

        attacker.Pieces[0].transform.LookAt(defender.Pieces[0].transform.position);
        defender.Pieces[0].transform.LookAt(attacker.Pieces[0].transform.position);
    }

    private void attack()
    {

    }

    private void move_to(HexComponent targetHexGO)
    {
        //Debug.Log("CC Moving From " + selectedUnit.Location.Name);
        int hexesMoved = this.selectedUnit.Move(targetHexGO.hex);
        //Debug.Log("CC Moving to " + selectedUnit.Location.Name);

        close();

    }



    private void HighLightHexs()
    {
        
        Game.map.UnHighLightHexs();

        List<Hex> hexs = Game.map.GetHexList();
        List<Hex> hexsToHighlight = new List<Hex>();

        foreach(Hex hex in hexs)
        {
            if(selectedUnit.Location.DistanceFrom(hex) <= selectedUnit.ActionPoints && selectedUnit.ValidMove(hex))
            {
                hexsToHighlight.Add(hex);
            }
        }
        hexsToHighlight.Add(selectedUnit.Location); 
        Game.map.HighLightHexs(hexsToHighlight);
        

    }

    private void UnHighLightHexs()
    {

        //Debug.Log(" CC Turning off highlight");
        //_filter.SetActive(false);
        //GameObject hexMap = Game.GetHexMapGo();
        //HexComponent hexGO;
        //foreach (Transform currentHex in hexMap.transform)
        //{
        //    hexGO = currentHex.gameObject.GetComponent<HexComponent>();
        //    foreach (Transform subItem in hexGO.transform)
        //    {
        //        subItem.gameObject.layer = 0;
        //    }
        //}

        Game.map.UpdateVisable();
    }
}
