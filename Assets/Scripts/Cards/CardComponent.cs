using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardComponent : MonoBehaviour
{

    public Card card;

    [SerializeField]
    public  GameObject PreFabs; 
    

    // Start is called before the first frame update
    void Start()
    {
        card = Game.GetCurrentPlayer().Hand.Cards[0];
        //Debug.Log("CardComp " + card.Name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
