using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string Name { get; set; }
    public CardZone Hand { get; set; }
    public CardZone Deck { get; set; }
    public CardZone Graveyard { get; set; }
    public List<Charge> Sources;
    public Charge Pool;
    public int Attunement;
    public int SourcePlays;
    public int SourcesPlayed;

    public List<Construct> AllUnits;

    public Unit Avatar;
    public List<Effect> ActiveEffects;

    public Player() {
        this.Deck = new TutorialDeck(this);
        this.Graveyard = new CardZone();
        this.Hand = new CardZone();
        this.Sources = new List<Charge>();
        this.Pool = new Charge();
        Hand.Capacity = 6;

        AllUnits = new List<Construct>();
        ActiveEffects = new List<Effect>();
        Avatar = new Avatar(this);
        AllUnits.Add(Avatar); 
        //placeAvatar();
        //Draw(5);
    }

    public int GainCharge(Charge charge) {
        charge.Index = Sources.Count;
        this.Sources.Add(charge);
        return 0;
    }

    public bool PaySetCost(Charge request) {
        for(int i = 0; i < this.Sources.Count; i++) {
            if(request.Slip == 0 && request.Essence == 0 
                && request.Holy == 0 && request.Unholy == 0) {
                return true;
            }
            if (!this.Sources[i].Tapped) {
                if (this.Sources[i].Slip > 0 && request.Slip > 0) {
                    request.Slip -= (request.Slip >= this.Sources[i].Slip) 
                        ? this.Sources[i].Slip : request.Slip;
                }
                if (this.Sources[i].Essence > 0 && request.Essence > 0) {
                    request.Essence -= (request.Essence >= this.Sources[i].Essence) 
                        ? this.Sources[i].Essence : request.Essence;
                }
                if (this.Sources[i].Holy > 0 && request.Holy > 0) {
                    request.Holy -= (request.Holy >= this.Sources[i].Holy) 
                        ? this.Sources[i].Holy : request.Holy;
                }
                if (this.Sources[i].Unholy > 0 && request.Unholy > 0) {
                    request.Unholy -= (request.Unholy >= this.Sources[i].Unholy) 
                        ? this.Sources[i].Unholy : request.Unholy;
                }
                if (this.Sources[i].TotalCharge(this.Sources[i]) == 0) {
                    this.Sources[i].Tapped = true;
                }
            }
        }
        return false;
    }

    private int CanPlaySource() {
        return SourcePlays - SourcesPlayed;
    }


    public void Draw(int amount) {
        Hand.GetNCardsFromZone(amount, Deck);
    }


    public virtual void OnTurnStart() {

        Draw(1);

        foreach(Unit construct in AllUnits) {
            construct.ActionPoints = 2;
        }
        //Debug.Log("Drew Another card");
    }

    public virtual void OnTurnEnd() { }


    public void placeAvatar() {
        Hex avatarLocation = Game.map.GetRandomHex();

        while(avatarLocation.type == Map.HexType.Water || avatarLocation.type == Map.HexType.High) {
            
            avatarLocation = Game.map.GetRandomHex();
        }

        Game.map.PlaceItem(Avatar, avatarLocation);

    }

}
