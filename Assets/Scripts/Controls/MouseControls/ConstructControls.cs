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
    msgQueue.AddCallback(Constants.SMSG_ATTACK, OnResponseAttack);
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
    Debug.Log("Clicked object: " + clickObject);
    Debug.Log("targetHexGO: " + targetHexGO);
    if(targetHexGO != null) {
      Debug.Log("RightClicked: targetHexGO valid");
      move_to(targetHexGO);
      return;
    }
    else{
        Debug.Log("Object clicked was null.");
    }
    UnitComponent targetUnitGO = clickObject.GetComponent<UnitComponent>();
    if (targetUnitGO != null) {
      // targetUnitGO.GetComponentInChildren<Knight_Animation_Controller>().TakeDamageAnimation();
      // selectedToMoveGO.GetComponentInChildren<Knight_Animation_Controller>().AttackAnimation();
      targetUnitGO.transform.LookAt(selectedToMoveGO.transform.position);
      selectedToMoveGO.transform.LookAt(targetUnitGO.transform.position);
      // Effect.PutOnStack(new AttackEffect(selectedUnit, targetUnitGO.unit, targetUnitGO, targetUnitGO));
      Debug.Log("Calling attack()");
      //changed to wrapper attack function
      attack(selectedUnit, targetUnitGO.unit);
      close();
    }
  }

  public static void PlayAttackAnimations(Unit attacker, Unit defender) {
    // defender.Pieces[0].GetComponentInChildren<Knight_Animation_Controller>().TakeDamageAnimation();
    // attacker.Pieces[0].GetComponentInChildren<Knight_Animation_Controller>().AttackAnimation();
    attacker.Pieces[0].transform.LookAt(defender.Pieces[0].transform.position);
    defender.Pieces[0].transform.LookAt(attacker.Pieces[0].transform.position);
  }

  private void attack(Unit attacker, Unit defender) {
    if (Game.networking){
      // Debug.Log("attacker id: " + Array.IndexOf(Game.players, attacker.Owner));
      // Debug.Log("defender id: " + Array.IndexOf(Game.players, defender.Owner));
      networkManager.SendAttackRequest(Array.IndexOf(Game.players, attacker.Owner), 
      attacker.Owner.Units.IndexOf(attacker), Array.IndexOf(Game.players, defender.Owner),
      defender.Owner.Units.IndexOf(defender));
      // Debug.Log("Sent AttackRequest");
    }else {
      // Debug.Log("Client side attack");
      attacker.Attack(defender);
      // Debug.Log("attacker id: " + Array.IndexOf(Game.players, attacker.Owner));
      // Debug.Log("defender id: " + Array.IndexOf(Game.players, defender.Owner));
      }
      close();
  }

  private void move_to(HexComponent targetHexGO) {
    //TODO
    // List<Hex> path = AStartPathfinding.AStartPath(selectedUnit, selectedUnit.Location, targetHexGO.hex);
    // StartCoroutine(MoveOneHexAtATime(selectedUnit, path ));
        if (Game.networking) {
            Debug.Log("ConstructControls: Sending MoveRequest");
            networkManager.SendMoveRequest(Game.GetCurrentPlayer().Units.IndexOf(selectedUnit),
            targetHexGO.hex.row, targetHexGO.hex.column);
        }
        else { 
            this.selectedUnit.Move(targetHexGO.hex); 
        }
    close();
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

        //Game.map.HighlightHexes(new List<Hex>());
        Game.map.UpdateVisible();
        Game.map.SelectHexes(hexesToHighlight);
  }

  private void UnhighlightHexes() {
    Game.map.UpdateVisible();
    Game.map.DeselectHexes();
  }

    IEnumerator MoveOneHexAtATime(Unit unit, List<Hex> moveList) {
        foreach (Hex move in moveList) {
            this.selectedUnit.Move(move);
            yield return new WaitForSeconds(.8f);
        }
    }


  //TODO conditionals only let avatar move
  public void OnResponseMove(ExtendedEventArgs eventArgs) {
    Debug.Log("OnResponseMove: begin");
	  ResponseMoveEventArgs args = eventArgs as ResponseMoveEventArgs;
	  if (args.user_id == Constants.OP_ID) {
	    Unit unit = (Unit) Game.GetCurrentPlayer().Units[args.piece_idx];
        unit.Move(Game.map.GetHex(args.x, args.y));
	  }
	  else if (args.user_id == Constants.USER_ID) {
      Game.GetCurrentPlayer().Avatar.Move(Game.map.GetHex(args.x, args.y));
  
	  }
	  else {
	  	Debug.Log("ERROR: Invalid user_id in ResponseReady: " + args.user_id);
	  }
  }

  public void OnResponseAttack(ExtendedEventArgs eventArgs){
    Debug.Log("Received attack callback");
  	ResponseAttackEventArgs args = eventArgs as ResponseAttackEventArgs;
    Debug.Log("args.attPid: " + args.attPid);
    Debug.Log("args.defPid: " + args.defPid);
    Debug.Log("args.attUid: " + args.attUid);
    Debug.Log("args.defUid: " + args.defUid);
    Player attacking = (Player) Game.players[args.attPid];
    Player defending = (Player) Game.players[args.defPid];
    Unit attacker = (Unit) attacking.Units[args.attUid];
    Unit defender = (Unit) defending.Units[args.defUid];
    attacker.Attack(defender);
  }
}
