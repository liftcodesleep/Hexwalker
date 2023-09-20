using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexControlls : MonoBehaviour, IMouseController
{
    public void LeftClicked(GameObject clickObject)
    {
        Debug.Log("Weee");
    }

    public void close()
    {
        throw new System.NotImplementedException();
    }

    public void open()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RightClicked(GameObject clickObject)
    {
        throw new System.NotImplementedException();
    }

    public MasterMouse.Task GetTask()
    {
        return MasterMouse.Task.StandBy;
    }
}
