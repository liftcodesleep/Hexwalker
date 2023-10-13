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
            throw new System.Exception("PlayCard, Card has no card component");
        }
    
        _filter = Game.GetFilter();
        

        // New
        _playableHexs = new List<HexComponent>();
        UnitDatabase data = cardGO.PreFabs.GetComponent<UnitDatabase>();
        

        cardPreFab = data.GetPrefab(cardGO.card.Name);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void clickedFromGUI()
    {
        Debug.Log("Play card Clicked");
        MasterMouse.leftClickObj(this.gameObject);
    }
    
    public void LeftClicked(GameObject clickedObject)
    {
        //MasterMouse.Selecteditems.Add(this.gameObject);
        //HighLightHexs();

        HighLightHexs();

    }
    public void open()
    {
        //UnitDatabase data = cardGO.PreFabs.GetComponent<UnitDatabase>();
        //SetPlayableHexs();
        //
        //
        //cardPreFab = data.GetPrefab(cardGO.card.Name);
        //
        //
        //
        //HighLightHexs();

        Debug.Log("In play card 1");
        switch (MasterMouse.currentTask)
        {

            case MasterMouse.Task.MoveUnit:
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
    public void close()
    {
    
        
        UnHighLightHexs();
        
        MasterMouse.taskOwner = null;
        MasterMouse.currentTask = MasterMouse.Task.StandBy;

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
