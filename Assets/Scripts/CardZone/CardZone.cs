using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CardZone
{
  static Random _random = new Random();
  public int Capacity{ get; set; }
  public List<Card> Cards;
  public Player Owner;

  public enum Types { Deck, Hand, Graveyard, InPlay };

  public CardZone() {
    this.Cards = new List<Card>();
  }

  public void Shuffle(List<Card> cards) {
    int n = cards.Count;
    for (int i = 0; i < (n - 1); i++) {
      int r = i + _random.Next(n - i);
      Card t = cards[r];
      cards[r] = cards[i];
      cards[i] = t;
    }
  }

  public Card PopCard() {
    Card topCard = Cards[0];
    Cards.Remove(topCard);
    return topCard;
  }

  public void GetNCardsFromZone(int n, CardZone TargetZone) {
    if (n > TargetZone.Cards.Count) {
      n = TargetZone.Cards.Count;
    }
    for (int i = 0; i < n; i++) {
      if (Capacity > Cards.Count) {
        Cards.Add(TargetZone.PopCard());
      }
    }
  }
}
