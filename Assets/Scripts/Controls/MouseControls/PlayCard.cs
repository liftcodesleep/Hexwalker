using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayCard : MonoBehaviour, IMouseController {
  private CardComponent cardGO;
  private GameObject cardPreFab;
  [SerializeField]
  private GameObject _filter;
  private List<HexComponent> _playableHexes;
  private NetworkManager networkManager;

  private void Start() {
    cardGO = this.gameObject.GetComponent<CardComponent>();
    if (cardGO == null) {
      throw new System.Exception("PlayCard, Card has no card component");
    }
    _filter = Game.GetFilter();
    // New
    _playableHexes = new List<HexComponent>();
    UnitDatabase data = cardGO.PreFabs.GetComponent<UnitDatabase>();
    cardPreFab = data.GetPrefab(cardGO.card.Name);
    if(cardPreFab == null) {
      throw new System.Exception("PlayCard " + cardGO.card.Name + " did not have a prefab");
    }
    networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
    MessageQueue msgQueue = networkManager.GetComponent<MessageQueue>();
    msgQueue.AddCallback(Constants.SMSG_SPAWN, OnResponseSpawn);
  }
  
  // Update is called once per frame
  void Update() {
  }
  
  public void clickedFromGUI() {
    //Debug.Log("Play card Clicked");
    MasterMouse.leftClickObj(this.gameObject);
  }
  
  public void LeftClicked(GameObject clickedObject) {
    Debug.Log("clickedObject: " + clickedObject);
    //MasterMouse.SelectedItems.Add(this.gameObject);
    //HighlightHexes();
    HighlightHexes();
  }

  public void open() {
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
    Debug.Log("clickObject: [" + clickObject.name +"]");
    HexComponent hexComp = clickObject.GetComponent<HexComponent>();
    if (hexComp == null) {
      Debug.Log("PlayCard: not a hex: [" + hexComp + "]");
      return;
    }
    Hex hex = hexComp.hex;
    if (hex == null) {
      Debug.Log("PlayCard: not a hex?!?!");
      return;
    }
    MasterMouse.SelectedItems.Add(clickObject);
    if(Play(cardGO.card, hex)) {
      Destroy(this.gameObject);
    }
    close();
  }


  public bool Play(Card card, Hex hex) {
    if (!(card.Cost <= card.Owner.Pool) ) {
      return false;
    }
    else {
      card.Owner.Pool -= card.Cost;
    // TODO:
    // Effect.PutOnStack(new PlayCardEffect(card, hex));
    }
    if(Game.networking){
      Debug.Log("server spawn: " + card.Name);
      Debug.Log("row: " + hex.row);
      Debug.Log("col: " + hex.column);
      Debug.Log("cardID: " + card.ID);
      networkManager.SendSpawnRequest(Array.IndexOf(Game.players, card.Owner), 
      hex.row, hex.column, card.ID);
    }
    else{ //Client side spawn
      Debug.Log("client spawn: " + card.Name);
      Debug.Log("row: " + hex.row);
      Debug.Log("col: " + hex.column);
      Debug.Log("cardID: " + card.ID);
      spawnPrefab(card, hex);
    }
    return true;
  }

  private static void spawnPrefab(Card card, Hex hex){
      UnitDatabase data = GameObject.Find("UnitSpellsDataBase").GetComponent<UnitDatabase>();
      if(data == null) {
        throw new System.Exception("Could not find DataBase in play card");
      }
      // Instantiate unit prefab
      GameObject unitGO = Instantiate(data.GetPrefab(card.Name), 
      Game.map.GetHexGO(hex).transform.position, Quaternion.identity, 
      Game.map.GetHexGO(hex).transform);
      card.Location = hex;
      //avatar looks toward summon location
      Game.GetCurrentPlayer().Avatar.Pieces[0].transform.LookAt(unitGO.transform.position);
      //summon animation
      Game.GetCurrentPlayer().Avatar.Pieces[0].GetComponent<UnitComponent>().HandleAttack();
      // TODO: Maybe should get unit to add to Constructs?
      // hex.Constructs.Add(card);
      if (unitGO.GetComponent<UnitComponent>()) {
        UnitComponent unitGOComp = unitGO.GetComponent<UnitComponent>();
        unitGOComp.unit = (Unit)card;
        unitGOComp.unit.Location = hex;
        card.Owner.Units.Add((Construct)card);

        if(card.type == Card.Type.UNIT)
            {
                hex.Constructs.Add((Unit)card);
            }
        
      }
      if (card.type == Card.Type.CHARGE) {
        //this.transform.localRotation = Quaternion.Euler(0, 60 * (int)Random.Range(0, 6), 0);
      }
      foreach (Effect currentEffect in card.ETBs) {
        currentEffect.ImmediateEffect();
      }
      Debug.Log("PlayCard finished playing!!!!!!!!!" + card.Name);
      card.Owner.Hand.Cards.Remove(card);
      card.Pieces.Add(unitGO);
  }

  private void HighlightHexes() {
    GameObject hexMap = Game.GetHexMapGo();
    _filter.SetActive(true);
    //HexComponent hexGO;
    //foreach ( Transform currentHex in hexMap.transform) {
    //  hexGO = currentHex.gameObject.GetComponent<HexComponent>();
    //  if (cardGO.card.IsPlayableHex(hexGO.hex)) {
    //    hexGO.gameObject.layer = 6;
    //    _playableHexes.Add(hexGO);
    //    foreach (Transform subItem in hexGO.transform) {
    //      subItem.gameObject.layer = 6;
    //    }
    //  }
    //  else {
    //    foreach (Transform subItem in hexGO.transform) {
    //      subItem.gameObject.layer = 0;
    //    }
    //  }
    //}
    List<Hex> hexList = new List<Hex>();
    foreach(Hex currentHex in Game.map.GetHexList())
        {
            if(cardGO.card.IsPlayableHex(currentHex))
            {
                hexList.Add(currentHex);
            }
        }
        Game.map.UpdateVisible();
        Game.map.SelectHexes(hexList);

    }
  
    public static void ResolveCard(Card card, Hex hex) {
        UnitDatabase data = GameObject.Find("UnitSpellsDataBase").GetComponent<UnitDatabase>();
        if (data == null) {
            throw new System.Exception("Could not find DataBase in play card");
        }
        GameObject cardPreFab = data.GetPrefab(card.Name);
        GameObject unitGO = Instantiate(cardPreFab, Game.map.GetHexGO(hex).transform.position, Quaternion.identity, Game.map.GetHexGO(hex).transform);
        Game.GetCurrentPlayer().Avatar.Pieces[0].transform.LookAt(unitGO.transform.position);
        Game.GetCurrentPlayer().Avatar.Pieces[0].GetComponent<UnitComponent>().HandleAttack();
        if (unitGO.GetComponent<UnitComponent>()) {
            UnitComponent unitGOComp = unitGO.GetComponent<UnitComponent>();
            unitGOComp.unit = (Unit)card;
            unitGOComp.unit.Location = hex;
            card.Owner.Units.Add((Construct)card);
            hex.Constructs.Add((Construct)card);
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
        Camera.main.GetComponent<CameraMovement>().MoveCamera(unitGO.transform);
        Game.map.UpdateVisible();
    }


  private void UnhighlightHexes() {
    Game.map.UpdateVisible();
       
        Game.map.DeselectHexes();
    }
  
  public MasterMouse.Task GetTask() {
    return MasterMouse.Task.PlayCard;
  }

  public void OnResponseSpawn(ExtendedEventArgs eventArgs) {
    Debug.Log("Received spawn callback");
    ResponseSpawnEventArgs args = eventArgs as ResponseSpawnEventArgs;
    Player caster = (Player)Game.players[args.pID];
    Hex hex = Game.map.GetHex(args.x, args.y);
    Card card = Card.cardDict[args.cardID];
    spawnPrefab(card, hex);
  }
}
