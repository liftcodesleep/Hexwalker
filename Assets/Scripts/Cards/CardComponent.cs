using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardComponent : MonoBehaviour
{

    public Card card;

    [SerializeField]
    public  GameObject PreFabs;


    [SerializeField]
    public TMPro.TMP_Text Title;

    [SerializeField]
    public TMPro.TMP_Text Type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCard(Card card)
    {
        this.card = card;
        Title.text = card.Name;
        Type.text = card.type.ToString();
    }
}
