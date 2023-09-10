using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Game
{
    private int _currentPlayer;
    public int _turnCount = 1;
    public PlayerData[] _players;

    // public Map map;
    // public Stack stack;

    public int CurrentPlayer
    {
        get { return current_player; }
        set { current_player = value; }
    }
}