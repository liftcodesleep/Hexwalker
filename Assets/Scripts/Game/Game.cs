using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// TODO: should this be monobehaviour/static maybe separate networking logic
// into another class?
public class Game : MonoBehaviour {
  private static int _currentPlayer = 0;
  public static int turnCount = 0;
  public static Player[] players;
  private static int HumanPlayer = 0;
  public static Map map;
  public static EffectStack stack;
  // public static Stack<Effect> stack;
  public static bool networking = true;
  private static GameObject HexMapGO;
  private static GameObject _filter;
  private NetworkManager networkManager;

  static Game() {
    //Player P1 = new Player();
    //Player P2 = new Player();
    //players = new Player[] {P1, P2};
    //P1.Name = "Player 1";
    //P2.Name = "Player 2";
    //P1.Deck = new TutorialDeck(P1);
    //P2.Deck = new TestDeck1(P2);
    //
    //stack = new EffectStack();
    //map.CurrentLevel.OnStartTurn(0);
  }

  public static Player GetCurrentPlayer() {
    return players[_currentPlayer % players.Length];
  }

  public static GameObject GetHexMapGo() {
    if (!HexMapGO) {
      HexMapGO = GameObject.Find("HexMap");
    }
    return HexMapGO;
  }

  public static GameObject GetFilter() {
    //return Camera.main.gameObject.transform.Find("Filter").gameObject; 
    if (_filter == null) {
      _filter = GameObject.Find("Main Camera");
      // _filter = _filter.transform.GetChild(0).gameObject;
    }
    return _filter;
  }

  public static void NextTurn() {
    foreach (Player player in players) {
      foreach(Effect currentEffect in player.ActiveEffects) {
        currentEffect.EndTurnEffect();
      }
      foreach( Construct currentConstruct in player.Units) {
        foreach(Effect currentEffect in currentConstruct.ActiveEffects) {
          currentEffect.EndTurnEffect();
        }
      }
    }
    // GetCurrentPlayer().OnTurnEnd();
    _currentPlayer++;
    GetCurrentPlayer().OnTurnStart();
    if(GetCurrentPlayer() == players[Game.GetHumanPlayer()]) turnCount++;
    map.CurrentLevel.OnStartTurn(turnCount);
  }

  public static void SetHumanPlayer(int value)
    {
        Debug.Log("Setting human to " + value);
        Game.HumanPlayer = value;
    }

    public static int GetHumanPlayer()
    {
        return Game.HumanPlayer;
    }
}
