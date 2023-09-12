using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public static class Game
{
    private static int _currentPlayer;
    public static int _turnCount = 1;
    public static Player[] _players;

    // public Map map;
    // public Stack stack;

    public static readonly int rows = 28;
    public static readonly int columns = 40;

    public static int  CurrentPlayer {get; set;}
}