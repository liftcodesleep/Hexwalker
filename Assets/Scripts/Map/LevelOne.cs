using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.PostProcessing.SubpixelMorphologicalAntialiasing;
using static UnityEngine.UI.GridLayoutGroup;

public class LevelOne : Level
{

    private Dictionary<string, Construct> _levelUnits;
    private Dictionary<string, GameObject> _levelEffects;

    private enum Stage { Starting, WalkedToFirst, PlayedMana, Tappedmana, PlayedUnit, AttackedUnit, TryPlayingASpell, PlayedSpell}
    private Stage _stage;
    public LevelOne() : base(1, "Tutorial", "Learn To play Hexwalker")
    {

        _stage = Stage.Starting;
        _levelUnits = new Dictionary<string, Construct>();
        _levelEffects = new Dictionary<string, GameObject>();

        _levelUnits.Add("Player", Game.players[0].Avatar);
        _levelUnits.Add("AI", Game.players[1].Avatar);
        _levelUnits.Add("Knight1", new Knight(Game.players[1]));
        _levelUnits.Add("Knight2", new Knight(Game.players[1]));
        _levelUnits.Add("Knight3", new Knight(Game.players[1]));
        _levelUnits.Add("Knight4", new Knight(Game.players[1]));
        _levelUnits.Add("Archer1", new Archer(Game.players[1]));
        _levelUnits.Add("Archer2", new Archer(Game.players[1]));
        _levelUnits.Add("Archer3", new Archer(Game.players[1]));




        //Instantiate(unitDatabase.GetPrefab("Death"), this.transform.position, Quaternion.identity);

    }

    public override void StartLevel()
    {
        //PlaceStartingUnits();
        Game.players[0].Draw(5);

        Game.map.PlaceItem(_levelUnits["Player"], Game.map.GetHex(14, 10));
        //owner.AllUnits.Add(this);
        Game.map.PlaceItem(_levelUnits["AI"], Game.map.GetHex(15, 35));
        

        GameObject moveMarker = Game.map.PlaceEffect("Marker", Game.map.GetHexGO(Game.map.GetHex(16, 8)).transform.position + Vector3.up * .2f);

        GameObject[] moveMarkers = 
            {
                //Game.map.PlaceEffect("Marker", Game.map.GetHexGO(Game.map.GetHex(14, 8)).transform.position + Vector3.up * .2f),
                Game.map.PlaceEffect("Marker", Game.map.GetHexGO(Game.map.GetHex(0, 0)).transform.position + Vector3.up * .2f),
                Game.map.PlaceEffect("Marker", Game.map.GetHexGO(Game.map.GetHex(0, 0)).transform.position + Vector3.up * .2f),
                Game.map.PlaceEffect("Marker", Game.map.GetHexGO(Game.map.GetHex(0, 0)).transform.position + Vector3.up * .2f),
                Game.map.PlaceEffect("Marker", Game.map.GetHexGO(Game.map.GetHex(0, 0)).transform.position + Vector3.up * .2f),
                Game.map.PlaceEffect("Marker", Game.map.GetHexGO(Game.map.GetHex(0, 0)).transform.position + Vector3.up * .2f),
                Game.map.PlaceEffect("Marker", Game.map.GetHexGO(Game.map.GetHex(0, 0)).transform.position + Vector3.up * .2f),

            };

        _levelEffects.Add("MoveMarker", moveMarker);


        Game.map.TalkingDialog.SetText( new string[]
            {
            "In a realm untethered by our understanding of reality, your "+
            "new life begins. A traumatic event has awakened latent power "+
            "within you. To begin your journey, move to the highlighted "+
            "area by left clicking the wizard and then right clicking the"+
            "highlighted area... ",

            "OH NO! Enemy forces! There is nothing we can do now! "+
            "Pass the turn by clicking the button in the bottom right hand "+
            "corner",

            "Quick! You need to harness the power of this world, play the "+
            "Connect to Nature Card. To play a card right click it, then "+
            "right click the hex you want to play it on ",

            "Good Job! To get a charge to play card you must first click the "+
            "area you connected to and then press the 'Tap Mana' button on "+
            "the menu that appears.",

            "Now that you have some mana, summon your Bear next to the "+
            "Knight by right clicking the Bear Card, and then click the area "+
            "you wish to summon it to.",

            "To attack, click on the Bear and then right click on to the "+
            "Knight.",

            "OH NO! It looks like they have called in some more units! "+
            "But this is all we can do now for now so pass the turn...",

            "Look at this new card that you just drew... It's a spell card, "+
            "you can cast these spells to buff your own units! Try playing it on your bear.",

            "Your unit is now stonger and healthier but this will only last for one turn!"
            }
            );
         
            
            
            }

    public override void OnStartTurn(int turn)
    {

        if(_stage == Stage.AttackedUnit)
        {
            _stage = Stage.TryPlayingASpell;
            Game.map.TalkingDialog.NextLine();
        }

        Debug.Log("sTARTING Turn " + turn);
        switch(turn)
        {
            case 0:
                //PlaceStartingUnits();
                break;
            case 1:
                //PlaceStartingUnits();
                //break;
            case 2:
                //PlaceStartingUnits();
                if (_stage == Stage.WalkedToFirst)
                {
                    _levelEffects["MoveMarker"].transform.position = Game.map.GetHexGO(Game.map.GetHex(15, 7)).transform.position + Vector3.up * .2f;
                    _levelEffects["MoveMarker"].SetActive(true);
                    CameraMovment.mainCamera.MoveCamera(Game.map.GetHexGO(Game.map.GetHex(15, 7)).transform);
                    Game.map.TalkingDialog.NextLine();
                    
                }
                break;
            default:
                return;
        }
    }

    public override void UpdateLevel()
    {
        
        if (_stage == Stage.Starting && Game.players[0].Avatar.Location == Game.map.GetHex(16, 8))
        {
            //Map.DestroyObject();
            _levelEffects["MoveMarker"].SetActive(false);
            Game.map.PlaceItem(_levelUnits["Knight1"], Game.map.GetHex(18, 8));
            ((Unit)_levelUnits["Knight1"]).Health = 1;
            CameraMovment.mainCamera.MoveCamera( Game.map.GetHexGO(Game.map.GetHex(18, 8)).transform );
            _stage = Stage.WalkedToFirst;
            Game.map.TalkingDialog.NextLine();

        }

        if (Game.map.GetHex(15, 7).cards.Count > 0 && _stage == Stage.WalkedToFirst)
        {
            //Map.DestroyObject();
            _levelEffects["MoveMarker"].SetActive(false);
            //Game.map.PlaceItem(_levelUnits["Knight2"], Game.map.GetHex(17, 5));
            _stage = Stage.PlayedMana;
            Game.map.TalkingDialog.NextLine();

        }

        if (_stage == Stage.PlayedMana && Game.players[0].Pool.Essence > 0)
        {
            _levelEffects["MoveMarker"].SetActive(false);
            
            _stage = Stage.Tappedmana;
            Game.map.TalkingDialog.NextLine();
        }

        if (_stage == Stage.Tappedmana && Game.players[0].AllUnits.Count > 3)
        {
            _levelEffects["MoveMarker"].SetActive(false);

            _stage = Stage.PlayedUnit;
            Game.map.TalkingDialog.NextLine();
        }

        if (_stage == Stage.PlayedUnit && ((Unit)_levelUnits["Knight1"]).currentZone == CardZone.Types.GraveYard)
        {
            _levelEffects["MoveMarker"].SetActive(false);
            Game.map.PlaceItem(_levelUnits["Knight2"], Game.map.GetHex(18, 6));
            Game.map.PlaceItem(_levelUnits["Knight3"], Game.map.GetHex(18, 8));
            _stage = Stage.AttackedUnit;
            Game.map.TalkingDialog.NextLine();
        }

        if (_stage == Stage.TryPlayingASpell)
        {
            bool found = false;
            foreach(Card card in Game.players[0].Hand.Cards) 
            {
                if (card.Name.Equals("GiantGrowth"))
                {
                    found = true; break;
                }
            }

            if (!found)
            {
                _levelEffects["MoveMarker"].SetActive(false);
                Game.map.PlaceItem(_levelUnits["Archer1"], Game.map.GetHex(18, 6));
                Game.map.PlaceItem(_levelUnits["Archer2"], Game.map.GetHex(18, 8));
                _stage = Stage.PlayedSpell;
                Game.map.TalkingDialog.NextLine();
            }
            
        }
    }
}
