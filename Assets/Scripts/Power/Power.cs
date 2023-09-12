using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power {
    public int Tapped;
    public int Magic;
    public int Mana;
    public int Holy;
    public int Unholy;

    public Power() {
        this.Tapped = 0;
        this.Magic = 0;
        this.Mana = 0;
        this.Holy = 0;
        this.Unholy = 0;
    }

    public static Power operator +(Power power1, Power power2)
    {
        Power power = new Power();
        power.Magic = power1.Magic + power2.Magic;
        power.Mana = power1.Mana + power2.Mana;
        power.Holy = power1.Holy + power2.Holy;
        power.Unholy = power1.Unholy + power2.Unholy;
        return power;
    }

    public static Power operator -(Power power1, Power power2)
    {
        Power power = new Power();
        power.Magic = power1.Magic - power2.Magic;
        power.Mana = power1.Mana - power2.Mana;
        power.Holy = power1.Holy - power2.Holy;
        power.Unholy = power1.Unholy - power2.Unholy;
        return power;
    }

    public int TotalPower(Power bank) {
        return bank.Mana + bank.Magic + bank.Holy + bank.Unholy;
    }
}
