using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayCard : MonoBehaviour, IMouseController
{
    private CardComponent cardGO;
    private GameObject cardPreFab;
    
    [SerializeField]
    private GameObject _filter;
    
    private List<HexComponent> _playableHexs;
    
    private void Start() {
        cardGO = this.gameObject.GetComponent<CardComponent>();
        if (cardGO == null) {
            throw new System.Exception("PlayCard, Card has no card component");
        }
    
        _filter = Game.GetFilter();
        

        // New
        _playableHexs = new List<HexComponent>();
        UnitDatabase data = cardGO.PreFabs.GetComponent<UnitDatabase>();
        

        cardPreFab = data.GetPrefab(cardGO.card.Name);

        if(cardPreFab == null) {
            throw new System.Exception("PlayCard " + cardGO.card.Name + " Did not have a prefab");
        }
    }
    
    // Update is called once per frame
    void Update() {
        
    }
    
    public void clickedFromGUI() {
        //Debug.Log("Play card Clicked");
        MasterMouse.leftClickObj(this.gameObject);
    }
    
    public void LeftClicked(GameObject clickedObject) {
        //MasterMouse.Selecteditems.Add(this.gameObject);
        //HighlightHexes();

        HighlightHexes();

    }
    public void open() {
        //UnitDatabase data = cardGO.PreFabs.GetComponent<UnitDatabase>();
        //SetPlayableHexs();
        //
        //
        //cardPreFab = data.GetPrefab(cardGO.card.Name);
        //
        //
        //
        //HighlightHexes();

        //Debug.Log("In play card 1");
        switch (MasterMouse.currentTask) {

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
    public void close() {
    
        
        UnhighlightHexes();
        
        MasterMouse.taskOwner = null;
        MasterMouse.currentTask = MasterMouse.Task.StandBy;

    }
    
    public void RightClicked(GameObject clickObject) {
        HexComponent hexComp = clickObject.GetComponent<HexComponent>();
        if (hexComp == null) {
            Debug.Log("PlayCard not a hex");
            return;
        }
        Hex hex = hexComp.hex;
        if (hex == null) {
            Debug.Log("PlayCard not a hex?!?!");
            return;
        }
        MasterMouse.Selecteditems.Add(clickObject);

        

        if( Play(cardGO.card,hex)) {
            Destroy(this.gameObject);
        }

        


        close();
        
    }


    public static bool Play(Card card, Hex hex) {

        if (!( card.Cost <= card.Owner.Pool) ) {
            
            return false;
        }
        else
        {
            card.Owner.Pool -= card.Cost;
        }
        
        

        UnitDatabase data = GameObject.Find("UnitSpellsDataBase").GetComponent<UnitDatabase>();

        if(data == null) {
            throw new System.Exception("Could not find DataBase in play card");
        }

        GameObject cardPreFab = data.GetPrefab(card.Name);

        GameObject unitGO = Instantiate(cardPreFab, Game.map.GetHexGO(hex).transform.position, Quaternion.identity, Game.map.GetHexGO(hex).transform);



        Game.GetCurrentPlayer().Avatar.Pieces[0].transform.LookAt(unitGO.transform.position);
        Game.GetCurrentPlayer().Avatar.Pieces[0].GetComponent<UnitComponent>().HandleAttack();


        hex.cards.Add(card);


        if (unitGO.GetComponent<UnitComponent>()) {
            UnitComponent unitGOComp = unitGO.GetComponent<UnitComponent>();
            unitGOComp.unit = (Unit)card;
            unitGOComp.unit.Location = hex;
            card.Owner.Units.Add((Construct)card);
        }
        if (card.type == Card.Type.CHARGE) {

            //this.transform.localRotation = Quaternion.Euler(0, 60 * (int)Random.Range(0, 6), 0);
        }

        card.Location = hex;
        foreach (Effect currentEffect in card.ETBs) {
            currentEffect.ImmediateEffect();

        }

        Debug.Log("PlayCard finished playing !!!!!!!!!!!! " + card.Name);
        card.Owner.Hand.Cards.Remove(card);

        card.Pieces.Add(unitGO);

        return true;
    }
    
 


    private void HighlightHexes() {
        GameObject hexMap = Game.GetHexMapGo();
        _filter.SetActive(true);

        HexComponent hexGO;
        foreach ( Transform currentHex in hexMap.transform) {
            
            hexGO = currentHex.gameObject.GetComponent<HexComponent>();

            
            if (cardGO.card.IsPlayableHex(hexGO.hex)) {
                
                hexGO.gameObject.layer = 6;
                _playableHexs.Add(hexGO);
    
                foreach (Transform subItem in hexGO.transform) {
                    subItem.gameObject.layer = 6;
                }
    
    
            }
            else
            {
                foreach (Transform subItem in hexGO.transform) {
                    subItem.gameObject.layer = 0;
                }
                
            }
            
        }
    }
    
    private void UnhighlightHexes() {
        //_filter.SetActive(false);
        //foreach (HexComponent hex in _playableHexs)
        //{
        //    foreach (Transform subItem in hex.transform)
        //    {
        //        subItem.gameObject.layer = 0;
        //    }
        //}

        Game.map.UpdateVisible();
        
    }
    
    
    public MasterMouse.Task GetTask() {
        return MasterMouse.Task.PlayCard;
    }
    

}
