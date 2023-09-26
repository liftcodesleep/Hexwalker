using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMouse : MonoBehaviour
{

    public static List<GameObject> Selecteditems;
    public static GameObject selectedItem;

    public enum Task { StandBy, PlayCard, MoveUnit }
    public static Task currentTask;

    private static IMouseController taskOwner;

    private static RaycastHit HitRay;
    private static Ray MouseRay;


    private static GameObject _filter;
    private static GameObject HexMapGO;


    // Start is called before the first frame update
    void Start()
    {
        Selecteditems = new List<GameObject>();
        currentTask = Task.StandBy;

        _filter = null;
    }

    // Update is called once per frame
    void Update()
    {

        if( Input.GetMouseButtonDown(0) )
        {
            selectedItem = getClickedObject();
            leftClick();
        }else if (Input.GetMouseButtonDown(1))
        {
            selectedItem = getClickedObject();
            rightClick();
        }


    }


    public static void leftClickObj(GameObject obj)
    {
        selectedItem = obj;
        leftClick();

    }

    private static void leftClick()
    {

        if (selectedItem ==null )
        {
            //Debug.Log("MM, Clicked nothing");
            taskOwner.close();
            return;
        }
        

        IMouseController mouseController;
        switch (currentTask)
        {
            case Task.StandBy:
                mouseController = selectedItem.GetComponent<IMouseController>();
                mouseController.LeftClicked(selectedItem);
                if (mouseController != null && mouseController.GetTask() != MasterMouse.Task.StandBy )
                {

                    SetTask(mouseController.GetTask(), mouseController);
                    taskOwner.open();
                   
                }
                break;
            case Task.PlayCard:
                taskOwner.close();
                SetTask(Task.StandBy, null);
                break;
            case Task.MoveUnit:
                //Debug.Log("Closing move");
                taskOwner.close();
                SetTask(Task.StandBy, null);
                break;
            default:
                SetTask(Task.StandBy, null);
                break;

        }

    }

    private void rightClick()
    {


        //GameObject selectedItem = getClickedObject();
        if (selectedItem == null)
        {
            //Debug.Log("MM, Clicked nothing");
            taskOwner.close();
            return;
        }

        //Debug.Log("MM " + MasterMouse.currentTask);
        IMouseController mouseController;
        switch (currentTask)
        {
            case Task.StandBy:
                
                mouseController = selectedItem.GetComponent<IMouseController>();
                mouseController.RightClicked(selectedItem);
                
                SetTask(mouseController.GetTask(), mouseController);
                break;
            case Task.PlayCard:
                //SetTask(Task.StandBy, null);
                Debug.Log(taskOwner.GetTask());
                taskOwner.RightClicked(selectedItem);
                break;
            case Task.MoveUnit:
                taskOwner.RightClicked(selectedItem);
                //Debug.Log("MM moving");
                //SetTask(Task.StandBy, null);
                break;
            default:
                SetTask(Task.StandBy, null);
                break;

        }

    }

    public static void SetTask(Task task, IMouseController owner)
    {
        if(taskOwner != null)
        {
            //Debug.Log("MM, Switching task " + currentTask);
            //taskOwner.close();
        }
        
        MasterMouse.taskOwner = owner;
        MasterMouse.currentTask = task;
        //Debug.Log("MM, Switching to " + task);

        if (owner != null)
        {
            //owner.open();
        }
        
    }


    private static GameObject getClickedObject()
    {
        MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(MouseRay, out HitRay, 100f))
        {
            //selectedItem = HitRay.transform.gameObject.transform.parent.transform.parent.gameObject;
            return HitRay.transform.gameObject;

        }

        return null;
    }


    

    


}
