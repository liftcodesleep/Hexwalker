using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Game
{
    private int _currentPlayer;
    public int _turnCount = 1;
    public Player[] _players;

    // public Map map;
    // public Stack stack;

    public int CurrentPlayer {get; set;}
}