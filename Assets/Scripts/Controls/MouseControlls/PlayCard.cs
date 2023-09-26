using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCard : MonoBehaviour, IMouseController
{
    private CardComponent cardGO;
    private GameObject cardPreFab;
    
    [SerializeField]
    private GameObject _filter;
    
    private List<HexComponent> _playableHexs;
    
    private void Start()
    {
        cardGO = this.gameObject.GetComponent<CardComponent>();
        if (cardGO == null)
        {
            Debug.Log("PlayCard, Card has no card component");
        }
    
        _filter = Game.GetFilter();
        //Debug.Log("PlayCard, f " + _filter.name);
        _playableHexs = new List<HexComponent>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void clickedFromGUI()
    {
        Debug.Log("Clicked");
        MasterMouse.leftClickObj(this.gameObject);
    }
    
    public void LeftClicked(GameObject clickedObject)
    {
        //MasterMouse.Selecteditems.Add(this.gameObject);
        //HighLightHexs();
    }
    public void open()
    {
        UnitDatabase data = cardGO.PreFabs.GetComponent<UnitDatabase>();
        SetPlayableHexs();
        
    
        //Debug.Log("PlayCard " + data.name);
        //Debug.Log("PlayCard " + cardGO.card.Name);
        cardPreFab = data.GetPrefab(cardGO.card.Name);

       

        HighLightHexs();

    }
    public void close()
    {
    
    
        
        
        UnHighLightHexs();
        
    }
    
    public void RightClicked(GameObject clickObject)
    {
        HexComponent hexComp = clickObject.GetComponent<HexComponent>();
        if (hexComp == null)
        {
            Debug.Log("PlayCard not a hex");
            return;
        }
        Hex hex = hexComp.hex;
        if (hex == null)
        {
            Debug.Log("PlayCard not a hex?!?!");
            return;
        }
        MasterMouse.Selecteditems.Add(clickObject);

        ////////////////////////////////////////////// NEED TO UPDATE ////////////////////////////////////////////

        

        GameObject unitGO = Instantiate(cardPreFab, clickObject.transform.position, Quaternion.identity, clickObject.transform);

        Game.GetCurrentPlayer().Avatar.Pieces[0].transform.LookAt(unitGO.transform.position);
        Game.GetCurrentPlayer().Avatar.Pieces[0].GetComponent<UnitComponent>().HandleAttack();

        hex.cards.Add(cardGO.card);
        UnitComponent unitGOComp = unitGO.GetComponent<UnitComponent>();
        if (unitGO)
        {
            unitGOComp.unit = (Unit)cardGO.card;
            unitGOComp.unit.Location = hexComp.hex;
        }
        if (cardGO.card.type == Card.Type.CHARGE)
        {
            
            this.transform.localRotation = Quaternion.Euler(0, 60 * (int)Random.Range(0, 6), 0);
        }

        Game.players[0].Hand.Cards.Remove(cardGO.card);

        Destroy(this.gameObject);

        cardGO.card.Pieces.Add(unitGO);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        close();
    }
    
    private void SetPlayableHexs()
    {
        _playableHexs = new List<HexComponent>();
    }
    
    private void HighLightHexs()
    {
        GameObject hexMap = Game.GetHexMapGo();
        _filter.SetActive(true);

        HexComponent hexGO;
        foreach ( Transform currentHex in hexMap.transform)
        {
            
            hexGO = currentHex.gameObject.GetComponent<HexComponent>();

            
            if (cardGO.card.IsPlayableHex(hexGO.hex))
            {
                
                hexGO.gameObject.layer = 6;
                _playableHexs.Add(hexGO);
    
                foreach (Transform subItem in hexGO.transform)
                {
                    subItem.gameObject.layer = 6;
                }
    
    
            }
            else
            {
                foreach (Transform subItem in hexGO.transform)
                {
                    subItem.gameObject.layer = 0;
                }
                
            }
            
        }
    }
    
    private void UnHighLightHexs()
    {
        _filter.SetActive(false);
        foreach (HexComponent hex in _playableHexs)
        {
            foreach (Transform subItem in hex.transform)
            {
                subItem.gameObject.layer = 0;
            }
        }
        
    }
    
    
    public MasterMouse.Task GetTask()
    {
        return MasterMouse.Task.PlayCard;
    }
    

}
