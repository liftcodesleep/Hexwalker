using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructControls : MonoBehaviour, IMouseController
{
  private UnitComponent selectedToMoveGO;
  private Unit selectedUnit;
  private GameObject _filter;
  private NetworkManager networkManager;


  private void Start() {
    networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
    MessageQueue msgQueue = networkManager.GetComponent<MessageQueue>();
    msgQueue.AddCallback(Constants.SMSG_MOVE, OnResponseMove);
    _filter = Game.GetFilter();
  }

  public void close() {
    selectedToMoveGO = null;
    UnhighlightHexes();
    MasterMouse.taskOwner = null;
    MasterMouse.currentTask = MasterMouse.Task.StandBy;
  }

  public MasterMouse.Task GetTask() {
    return MasterMouse.Task.MoveUnit;
  }

  public void LeftClicked(GameObject clickObject) {
    //Debug.Log("CC LC");
    selectedToMoveGO = clickObject.GetComponent<UnitComponent>();
    if(!selectedToMoveGO) {
      //Debug.Log("Construct Controls: Not unit clicked");
      return;
    }
    selectedUnit = selectedToMoveGO.unit;
    if (!selectedToMoveGO) {
      Debug.Log("Unit was Construct was null In left click");
      throw new NullReferenceException("Unit was Construct was null In left click");
    }
    HighlightHexes();
  }

  public void open() {
    //Debug.Log("CC open");
    switch (MasterMouse.currentTask) {
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

  public void RightClicked(GameObject clickObject) {
    HexComponent targetHexGO = clickObject.GetComponent<HexComponent>();
    Debug.Log("RightClicked: received targetHexGO");
    if(targetHexGO != null) {
      Debug.Log("RightClicked: targetHexGO valid");
      move_to(targetHexGO);
      return;
    }
    else{
        Debug.Log("ur dum");
    }
    UnitComponent targetUnitGO = clickObject.GetComponent<UnitComponent>();
    if (targetUnitGO != null) {
      targetUnitGO.GetComponentInChildren<Knight_Animation_Controller>().TakeDamageAnimation();
      selectedToMoveGO.GetComponentInChildren<Knight_Animation_Controller>().AttackAnimation();
      targetUnitGO.transform.LookAt(selectedToMoveGO.transform.position);
      selectedToMoveGO.transform.LookAt(targetUnitGO.transform.position);
      selectedUnit.Attack(targetUnitGO.unit);
      close();
    }
  }

  public static void PlayAttackAnimations(Unit attacker, Unit defender) {
    defender.Pieces[0].GetComponentInChildren<Knight_Animation_Controller>().TakeDamageAnimation();
    attacker.Pieces[0].GetComponentInChildren<Knight_Animation_Controller>().AttackAnimation();
    attacker.Pieces[0].transform.LookAt(defender.Pieces[0].transform.position);
    defender.Pieces[0].transform.LookAt(attacker.Pieces[0].transform.position);
  }

  private void attack() { }

  private void move_to(HexComponent targetHexGO) {
    Debug.Log("move_to: Moving!\n");
    if (Game.networking) {
      Debug.Log("move_to: Networking!\n");
      networkManager.SendMoveRequest(Game.GetCurrentPlayer().Units.IndexOf(selectedUnit), 
      targetHexGO.hex.row, targetHexGO.hex.column);
      Debug.Log("move_to: Networked!\n");
    }
    this.selectedUnit.Move(targetHexGO.hex);
    close();
    Debug.Log("move_to: Done!\n");
  }

  private void HighlightHexes() {
    Game.map.UnhighlightHexes();
    List<Hex> hexes = Game.map.GetHexList();
    List<Hex> hexesToHighlight = new List<Hex>();
    foreach(Hex hex in hexes) {
      if(selectedUnit.Location.DistanceFrom(hex) <= selectedUnit.ActionPoints && selectedUnit.ValidMove(hex)) {
        hexesToHighlight.Add(hex);
      }
    }
    hexesToHighlight.Add(selectedUnit.Location); 
    Game.map.HighlightHexes(hexesToHighlight);
  }

  private void UnhighlightHexes() {
    Game.map.UpdateVisible();
  }

  public void OnResponseMove(ExtendedEventArgs eventArgs) {
    Debug.Log("OnResponseMove: begin");
	ResponseMoveEventArgs args = eventArgs as ResponseMoveEventArgs;
	if (args.user_id == Constants.OP_ID) {
      Debug.Log("OnResponseMove: moving");
      Debug.Log("OnResponseMove: UnitID: " + args.piece_idx.ToString());
      Debug.Log("OnResponseMove: Hex Col: " + args.x.ToString());
      Debug.Log("OnResponseMove: Hex Row: " + args.y.ToString());
	  Unit unit = (Unit) Game.GetCurrentPlayer().Units[args.piece_idx];
      unit.Move(Game.map.GetHex(args.x, args.y));
    //   Game.players[0].Avatar.Move(Game.map.GetHex(args.x, args.y));
	}
	else if (args.user_id == Constants.USER_ID) {
	}
	else {
		Debug.Log("ERROR: Invalid user_id in ResponseReady: " + args.user_id);
	}
  }
}
