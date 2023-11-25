using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilty : MonoBehaviour, IMouseController
{

    private GameObject _filter;

    void Start() {
        _filter = Game.GetFilter();
    }

    public static List<Hex> TargetHexes() {


        return null;
    }

    public MasterMouse.Task GetTask() {
        throw new System.NotImplementedException();
    }

    public void open() {
        throw new System.NotImplementedException();
    }

    public void LeftClicked(GameObject clickObject) {
        throw new System.NotImplementedException();
    }

    public void RightClicked(GameObject clickObject) {
        throw new System.NotImplementedException();
    }

    public void close() {
        throw new System.NotImplementedException();
    }
}
