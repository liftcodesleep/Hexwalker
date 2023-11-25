using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AI : Player {
  public AI() {
    this.Deck = new AIDeck(this);
  }

  public override void OnTurnStart() {
    Game.map.StartCoroutine(HandleTurn());
  }

  public override void OnTurnEnd() { }

  IEnumerator HandleTurn() {
    Draw(1);
    foreach (Unit construct in AllUnits) {
      construct.ActionPoints = 2;
    }
    foreach (Unit construct in AllUnits) {
      if (AIAttack(construct) ) yield return new WaitForSeconds(1f);
      MoveUnitToLocation(construct, Game.players[0].Avatar.Location);
      yield return new WaitForSeconds(2f);
      if (AIAttack(construct)) yield return new WaitForSeconds(1f);
    }
    Debug.Log("Ending AI TURN");
    Game.NextTurn();
  }

  IEnumerator Move() {
    List<Hex> path = FindPath(this.Avatar, Game.players[0].Avatar.Location, new List<Hex>());
    Debug.Log("The found path size was " + path.Count);
    foreach (Hex hex in path) {
      yield return new WaitForSeconds(1f);
      this.Avatar.Move(hex);
    }
  }

  /*
   * Just moves closer to the target. Does not find true path
   */
  public void MoveUnitToLocation(Unit unit, Hex target_location) {
    Hex startingLocation = unit.Location;
    for (int direction_index = 0; direction_index < 6; direction_index++) {
      Hex newLocation = Game.map.GetAdjacentHex(unit.Location, (Map.Direction)direction_index);
      if (newLocation == null) continue;
      if (newLocation.DistanceFrom(target_location) < startingLocation.DistanceFrom(target_location)) {
        //Debug.Log(newLocation.row + " " + newLocation.column + " " + unit.ValidMove(newLocation));
        if (unit.ValidMove(newLocation) && newLocation.cards.Count == 0) {
          unit.Move(newLocation);
          return;
        }
      }
    }

    /// This is bad just here for checking movement when distance is equal
    for (int direction_index = 0; direction_index < 6; direction_index++) {
      Hex newLocation = Game.map.GetAdjacentHex(unit.Location, (Map.Direction)direction_index);
      if (newLocation == null) continue;
      if (newLocation.DistanceFrom(target_location) <= startingLocation.DistanceFrom(target_location)) {
        Debug.Log(newLocation.row + " " + newLocation.column + " " + unit.ValidMove(newLocation));
        if (unit.ValidMove(newLocation) && newLocation.cards.Count == 0) {
          unit.Move(newLocation);
          return;
        }
      }
    }
    Debug.Log("!!!!!!!!!! AI Cound not find a good path");
    //while (startingLocation != this.Avatar.Location)
    //{
    //  this.Avatar.Move(Game.map.GetAdjacentHex(this.Avatar.Location, (Map.Direction)Random.Range(0, 5)));
    //}
    this.Avatar.Move(Game.map.GetAdjacentHex(this.Avatar.Location, (Map.Direction)Random.Range(0, 5)));
  }


  public List<Hex> FindPath(Unit unit, Hex destantion, List<Hex> currentPath) {
    if(currentPath.Count > 0 && currentPath.Last() == destantion) {
      Debug.Log("Got the path!! its size is " + currentPath.Count);
      return currentPath;
    }
    /*
     * Tests the path for every hex next to the current hex
     * Do another recursive call if the hex that it was nex to was not already in the list
     */
    Hex newLocation;
    List<Hex> newPath;
    List<Hex>[] allPaths = new List<Hex>[6];
    for (int direction_index = 0; direction_index < 6; direction_index++) {
      newLocation = Game.map.GetAdjacentHex(unit.Location, (Map.Direction)direction_index);
      if(currentPath.Contains(newLocation)) {
        continue;
      }
      newPath = new List<Hex>(currentPath);
      newPath.Add(newLocation);
      allPaths[direction_index] = FindPath(unit, destantion, newPath);
    }
    // If weighted paths change this section
    newPath = allPaths[0];
    foreach (List<Hex> path in allPaths) {
      if(path != null && path.Count < newPath.Count) {
        newPath = path;
      }
    }
    return newPath;
  }

  public bool AIAttack(Unit testUnit) {
    //Unit testUnit = (Unit)Game.players[1].AllUnits[2];
    for (int direction_index = 0; direction_index < 6; direction_index++) {
      Hex newLocation = Game.map.GetAdjacentHex(testUnit.Location, (Map.Direction)direction_index);
      if (newLocation == null) continue;
      if (newLocation.cards.Count != 0 && newLocation.cards[0].Owner != this) {
        Debug.Log("Attacking!!!!!!!!!!");
        ConstructControls.PlayAttackAnimations(testUnit, (Unit)newLocation.cards[0]);
        testUnit.Attack((Unit)newLocation.cards[0]);
        return true;
      }
    }
    return false;
  }
}
