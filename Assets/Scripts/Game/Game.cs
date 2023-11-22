using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public static class Game
{
    private static int _currentPlayer = 0;
    public static int turnCount = 0;
    public static Player[] players;

    public static Map map;
    public static Stack<Effect> stack;

    public static readonly int rows = 26;
    public static readonly int columns = 38;

    private static GameObject HexMapGO;
    private static GameObject _filter;

    static Game()
    {
        players = new Player[] { new Player(), new AI() };

        players[0].Name = "Player 1";
        players[1].Name = "Player 2";

        stack = new Stack<Effect>();

        //map.CurrentLevel.OnStartTurn(0);

    }

    public static Player GetCurrentPlayer() 
    {
        return players[_currentPlayer % players.Length];
    }


    public static GameObject GetHexMapGo()
    {

        if (!HexMapGO)
        {
            HexMapGO = GameObject.Find("HexMap");
        }
        return HexMapGO;
    }

    public static GameObject GetFilter()
    {
        //return Camera.main.gameObject.transform.Find("Filter").gameObject; 

        if (_filter == null)
        {
            _filter = GameObject.Find("Main Camera");
            _filter = _filter.transform.GetChild(1).gameObject;

        }
        return _filter;
    }


    public static void NextTurn()
    {

        
        foreach (Player player in players)
        {
            foreach(Effect currentEffect in player.ActiveEffects) 
            {
                currentEffect.EndTurnEffect();
            }

            foreach( Construct currentConstruct in player.AllUnits)
            {
                foreach(Effect currentEffect in currentConstruct.ActiveEffects)
                {
                    currentEffect.EndTurnEffect();
                }
            }
        }
        GetCurrentPlayer().OnTurnEnd();
        _currentPlayer++;
        GetCurrentPlayer().OnTurnStart();

        if(GetCurrentPlayer() == players[0]) turnCount++;

        map.CurrentLevel.OnStartTurn(turnCount);
    }

}