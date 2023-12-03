using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : Construct {
  //List<Effect> UnitEffects;
  public int HealthPoints = 1;
  public int Health = 1;
  public int AttackCost = 1;
  public int MoveCost = 1;
  public int ActionPoints = 1;
  public int Actions = 1;
  public int Strength = 1;
  public int SummonRange = 2;

  public Map.HexType[] moveableHexTypes;

  public bool init_unit(){
    DontDestroyOnLoad(gameObject);
		networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
		MessageQueue msgQueue = networkManager.GetComponent<MessageQueue>();
		msgQueue.AddCallback(Constants.SMSG_MOVE, OnResponseMove);
  }

  public Unit(Player Owner) : base(Owner) {
    type = Card.Type.UNIT;
  }

  public void OnDeath() {
    Location = null;
    currentZone = CardZone.Types.GraveYard;
    Owner.Units.Remove(this);
  }

  public int Move(Hex hexTarget) {
    if (!ValidMove(hexTarget)) {
      return 0;
    }
    int hexesTraveled = (int)Location.DistanceFrom(hexTarget);
    Location.cards.Remove(this);
    Location = hexTarget;
    hexTarget.cards.Add(this);
    ActionPoints -= hexesTraveled * MoveCost;
    return hexesTraveled;
  }

  public void Attack(Unit target) {
    target.TakeDamage( this.Strength );
  }

  
  // TODO: Damage should probably be an effect something like 
  // Damage(int amount, Unit target)
  // target.Health -= damage;
  // at this point we could check if health <= 0, but better logic might be
  // a constant update on state-based-actions that evaluates things like this
  // so evaluating death and similar is done separately and elsewhere
  public void TakeDamage(int damage_amount) {
    this.Health -= damage_amount;
    if(Health <= 0) {
      this.currentZone = CardZone.Types.GraveYard;
      Location = null;
      Debug.Log("Unit was put into the graveyard");
    }
  }

  public override string ToString() {
    return base.ToString() +
      "Health: " + Health + "\n" +
      "AttackCost: " + AttackCost + "\n" +
      "MoveCost: " + MoveCost + "\n" +
      "ActionPoints: " + ActionPoints + "\n" +
      "Actions: " + Actions + "\n"+
      "Strength: " + Strength + "\n" ;
  }

  public bool ValidMove(Hex hex) {
    bool valid = false;
    if (hex.type == Map.HexType.Water || hex.type == Map.HexType.High) {
      return false;
    }
    foreach(Map.HexType currentType in moveableHexTypes) {
      if(currentType == hex.type) {
        valid = true;
        break;
      }
    }
    if(!valid) {
      return false;
    }
    //if(hex.cards.Count  > 0)
    //{
    //  return false;
    //}

    //if (hex.cards.Count > 0 && hex.cards[0].Owner != this.Owner)
    //{
    //  return false;
    //}

    if (this.Location.DistanceFrom(hex) > this.ActionPoints) {
      return false;
    }
    return true;
  }

  public void OnResponseMove(ExtendedEventArgs eventArgs) {
		ResponseMoveEventArgs args = eventArgs as ResponseMoveEventArgs;
		if (args.user_id == Constants.OP_ID) {
			int pieceIndex = args.piece_idx;
			int x = args.x;
			int y = args.y;
			Unit unit = Game.players[args.user_id - 1].Heroes[pieceIndex];
			gameBoard[unit.x, unit.y] = null;
			unit.Move(x, y);
			gameBoard[x, y] = unit;
		}
		else if (args.user_id == Constants.USER_ID) {
			// Ignore
		}
		else
		{
			Debug.Log("ERROR: Invalid user_id in ResponseReady: " + args.user_id);
		}
	}

}
