using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            if (selectedUnit == targetUnitGO.unit) return;

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

        List<Hex> path = AStartPathfinding.AStartPath(selectedUnit, selectedUnit.Location, targetHexGO.hex);
        StartCoroutine(MoveOneHexAtATime(selectedUnit, path ));
        //int hexesMoved = this.selectedUnit.Move(targetHexGO.hex);

        
        //if(hexesMoved > 0)
        //{
        //    Camera.main.GetComponent<CameraMovment>().MoveCamera(targetHexGO.transform);
        //}

        close();

    }

    IEnumerator MoveOneHexAtATime(Unit unit, List<Hex> moveList)
    {

        
        foreach (Hex move in moveList)
        {
            this.selectedUnit.Move(move);
            
            yield return new WaitForSeconds(.8f);

        }
        
    }

    private void HighLightHexs()
    {
        
        Game.map.UnHighLightHexs();

        List<Hex> hexs = Game.map.GetHexList();
        List<Hex> hexsToHighlight = new List<Hex>();

        foreach(Hex hex in hexs)
        {
            if (  selectedUnit.Location.DistanceFrom(hex) <= selectedUnit.ActionPoints && selectedUnit.ValidMove(hex))
            {

                List<Hex> path = AStartPathfinding.AStartPath(selectedUnit, selectedUnit.Location, hex);
                if(path.Count-1 <= selectedUnit.ActionPoints)
                {
                    hexsToHighlight.Add(hex);
                }
                
            }
        }
        hexsToHighlight.Add(selectedUnit.Location);

        Game.map.SelectHexs(hexsToHighlight);
        Game.map.UpdateVisable();


    }

    private void UnHighLightHexs()
    {


        Game.map.DeSelectHexes();
        Game.map.UpdateVisable();
    }
}
