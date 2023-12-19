using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.PostProcessing.SubpixelMorphologicalAntialiasing;
using static UnityEngine.UI.GridLayoutGroup;

public class TestLevel : Level
{

    private Dictionary<string, Construct> _levelUnits;
    private Dictionary<string, GameObject> _levelEffects;
    public TestLevel() : base(1, "Test", "Demo") {

        Player P1 = new Player();
        Player P2 = new Player();
        Game.players = new Player[] { P1, P2 };
        P1.Name = "Player 1";
        P2.Name = "Player 2";
        P1.Deck = new TutorialDeck(P1);
        P2.Deck = new TestDeck1(P2);

        Game.stack = new EffectStack();

        _levelUnits = new Dictionary<string, Construct>();
        _levelEffects = new Dictionary<string, GameObject>();
        _levelUnits.Add("Player", Game.players[Game.GetHumanPlayer()].Avatar);
        _levelUnits.Add("AI", Game.players[(Game.GetHumanPlayer()+1)%2].Avatar);
    }

    public override void StartLevel() {



        Game.players[Game.GetHumanPlayer()].Draw(5);
        //Debug.Log("Player " + (Game.GetHumanPlayer() + 1) % 2 + " Drawing 5 cards");
        Game.players[(Game.GetHumanPlayer() + 1) % 2].Draw(5);
        //Debug.Log("Done drawing cards");

        Game.map.PlaceItem(_levelUnits["Player"], Game.map.GetHex(14, 10));
        Game.map.PlaceItem(_levelUnits["AI"], Game.map.GetHex(14, 11));
    }

    public override void OnStartTurn(int turn){}
    public override void UpdateLevel(){}
}

