using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPanelComponent : MonoBehaviour
{
  CardZone oldHand;
  CardZone playerHand;

  [SerializeField]
  private GameObject cardGO;

  private float movementSpeed = 6f;

  void Start() {
    oldHand = new CardZone(); 
    playerHand = Game.players[Game.GetHumanPlayer()].Hand;

    updateCards();
  }

  
  void Update() {

    
    int current_child = 0;
    while(current_child < this.transform.childCount) {
      if(this.transform.GetChild(current_child).localPosition.x > current_child - 3) {
        this.transform.GetChild(current_child).position += Vector3.left * movementSpeed;
      }
      current_child++;
    }

    updateCards();


  }





  private List<Card> AddedCards() {
    List<Card> addedCards = new List<Card>();
    bool missCard = true;
    foreach(Card newCard in playerHand.Cards) {
      foreach ( Card oldCard in oldHand.Cards) {
        missCard = true;
        if (ReferenceEquals(newCard, oldCard) ) {
          missCard = false;
          break;
        }
      }

      if(missCard) {
        addedCards.Add(newCard);
        oldHand.Cards.Add(newCard);
      }
      
      

    }

    return addedCards;
  }


  public void updateCards() {
    //Debug.Log("Updating Card Panel Cards in hand " + playerHand.Cards.Count);
    List<Card> addedCards = AddedCards();
    if (addedCards.Count > 0) {

      GameObject newCardGO;
      CardComponent newCardComp;
      foreach (Card card in addedCards) {
        newCardGO = Instantiate(cardGO, this.gameObject.transform.position + Vector3.right*400, Quaternion.identity, this.gameObject.transform);
        newCardComp = newCardGO.GetComponent<CardComponent>();
        //newCardComp.card = card;
        //newCardComp.Title.text = card.Name;
        newCardComp.SetCard(card);

      }
    }
  }


  IEnumerator DrawCardsAnimation(List<Card> addedCards) {
    GameObject newCardGO;
    CardComponent newCardComp;
    foreach(Card card in addedCards) {
      newCardGO = Instantiate(cardGO, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
      newCardComp = newCardGO.GetComponent<CardComponent>();
      newCardComp.card = card;
      newCardComp.Title.text = card.Name;

      while(true) {
        yield return new WaitForSeconds(movementSpeed);
        newCardGO.transform.position += Vector3.left*2;

      }

    }


    //transform.SetParent(Game.map.gameObject.transform.GetChild(0).transform);

  }


}
