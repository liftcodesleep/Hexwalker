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
        _levelUnits = new Dictionary<string, Construct>();
        _levelEffects = new Dictionary<string, GameObject>();
        _levelUnits.Add("Player", Game.players[Game.HumanPlayer].Avatar);
        _levelUnits.Add("AI", Game.players[1].Avatar);
    }

    public override void StartLevel() {
        Game.players[Game.HumanPlayer].Draw(5);
        Game.map.PlaceItem(_levelUnits["Player"], Game.map.GetHex(14, 10));
        Game.map.PlaceItem(_levelUnits["AI"], Game.map.GetHex(15, 35));
    }

    public override void OnStartTurn(int turn){}
    public override void UpdateLevel(){}
}

