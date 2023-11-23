using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMouseController
{

    public MasterMouse.Task GetTask(); 
    public void open();

    public void LeftClicked(GameObject clickObject);
    public void RightClicked(GameObject clickObject);
    public void close();

}
