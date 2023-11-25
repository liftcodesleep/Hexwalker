using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexControls : MonoBehaviour, IMouseController
{
    public void LeftClicked(GameObject clickObject)
    {
        
    }

    public void close()
    {
        
    }

    public void open()
    {
        
        switch (MasterMouse.currentTask)
        {

            case MasterMouse.Task.StandBy:
                //Debug.Log("In hex control 1");
                MasterMouse.taskOwner = this;
                
                break;


            /*
             * Checks to see if you are clicking the menu weird delay for some reson
             */
            case MasterMouse.Task.MoveUnit:
                MasterMouse.currentTask = MasterMouse.Task.UnitMenuClicked;
                StartCoroutine(UpdateFromUnit());
                break;
            

            

            case MasterMouse.Task.Transition:
                MasterMouse.taskOwner.close();
                MasterMouse.currentTask = GetTask();
                MasterMouse.taskOwner = this;
                //Debug.Log("In hex control 2");
                break;

            default:
                MasterMouse.currentTask = MasterMouse.Task.Transition;
                //Debug.Log("In hex control 3");
                break;

        }



    }

    private IEnumerator UpdateFromUnit()
    {

        yield return new WaitForSeconds(.1f);

        if(MasterMouse.currentTask != MasterMouse.Task.UnitMenuClicked )
        {
            if(MasterMouse.taskOwner != null) MasterMouse.taskOwner.close();
            MasterMouse.currentTask = GetTask();
            MasterMouse.taskOwner = this;
        }
        
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
        
    }

    public MasterMouse.Task GetTask()
    {
        return MasterMouse.Task.StandBy;
    }

    
}
