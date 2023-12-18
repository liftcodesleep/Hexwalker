using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card {
  private static int cardID = 0;
  public static Dictionary<int, Card> cardDict = new Dictionary<int, Card>();
  public int ID;
  public Charge Cost;
  public string Name;
  public string Rules;
  public string Flavor;
  public int Priority;
  public int Range;
  public int IsCard;
  public Hex Location;
  public Player Owner;
  public bool Tapped;
  public List<GameObject> Pieces;
  public List<Effect> ETBs;
  public CardZone.Types currentZone;

  public Card(Player Owner) {
    this.Owner = Owner;
    this.Cost = new Charge();
    this.Pieces = new List<GameObject>();
    this.Tapped = false;
    this.ETBs = new List<Effect>();
    this.currentZone = CardZone.Types.Deck;
    cardID++;
    cardDict[cardID] = this;
    this.ID = cardID;
  }

  public enum Type { CHARGE, UNIT, ARTIFACT, SPELL  }

  public Type type;

  public abstract bool IsPlayableHex(Hex hex);

  public override string ToString() {
    return "Name: " + Name + "\n" +
      "Range: " + Range + "\n";
  }


}
