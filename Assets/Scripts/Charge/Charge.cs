using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Charge {
    public int Slip {get; set;}
    public int Essence {get; set;}
    public int Holy {get; set;}
    public int Unholy {get; set;}
    public int Index { get; set;}
    public bool Tapped { get; set; }

    public Charge() {
        this.Slip = 0;
        this.Essence = 0;
        this.Holy = 0;
        this.Unholy = 0;
        this.Tapped = false;
    }
    public Charge(int slip, int essence, int holy, int unholy, bool tapped) {
        this.Slip = slip;
        this.Essence = essence;
        this.Holy = holy;
        this.Unholy = unholy;
        this.Tapped = tapped;
    }


    public static Charge operator +(Charge charge1, Charge charge2) {
        Charge charge = new Charge();
        charge.Slip = charge1.Slip + charge2.Slip;
        charge.Essence = charge1.Essence + charge2.Essence;
        charge.Holy = charge1.Holy + charge2.Holy;
        charge.Unholy = charge1.Unholy + charge2.Unholy;
        return charge;
    }

    public static Charge operator -(Charge charge1, Charge charge2) {
        Charge charge = new Charge();
        charge.Slip = charge1.Slip - charge2.Slip;
        charge.Essence = charge1.Essence - charge2.Essence;
        charge.Holy = charge1.Holy - charge2.Holy;
        charge.Unholy = charge1.Unholy - charge2.Unholy;
        return charge;
    }

    public static bool operator ==(Charge charge1, Charge charge2) {
        return (charge1.Unholy == charge2.Unholy) && (charge1.Holy == charge2.Holy)
    && (charge1.Slip == charge2.Slip) && (charge1.Essence == charge2.Essence);
    }

    public static bool operator !=(Charge charge1, Charge charge2) {
        return (charge1.Unholy != charge2.Unholy) || (charge1.Holy != charge2.Holy)
    || (charge1.Slip != charge2.Slip) || (charge1.Essence != charge2.Essence);

    }

    public override bool Equals(System.Object charge) {
        if ((charge == null) || !this.GetType().Equals(charge.GetType())) {
            return false;
        }
        Charge charge1 = (Charge) charge;
        return charge1.Unholy == this.Unholy && charge1.Holy == this.Holy
            && charge1.Slip == this.Slip && charge1.Essence == this.Essence;
    }

    public override int GetHashCode() {
        //fake hashing but unneeded
        return (this.TotalCharge(this) << 2);
    }

    public int TotalCharge(Charge bank) {
        return bank.Essence + bank.Slip + bank.Holy + bank.Unholy;
    }


    public static bool operator >=(Charge charge1, Charge charge2) {
        return (charge1.Unholy >= charge2.Unholy) && (charge1.Holy >= charge2.Holy)
    && (charge1.Slip >= charge2.Slip) && (charge1.Essence >= charge2.Essence);

    }
    public static bool operator <=(Charge charge1, Charge charge2) {
        Debug.Log(charge1.Essence + " " + charge2.Essence);
        Debug.Log("The value for <= " + ((charge1.Unholy <= charge2.Unholy) && (charge1.Holy <= charge2.Holy)
    && (charge1.Slip <= charge2.Slip) && (charge1.Essence <= charge2.Essence)));
        return (charge1.Unholy <= charge2.Unholy) && (charge1.Holy <= charge2.Holy)
    && (charge1.Slip <= charge2.Slip) && (charge1.Essence <= charge2.Essence);

    }

    
}
