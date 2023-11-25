using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMouse : MonoBehaviour
{

    public static List<GameObject> Selecteditems;
    public static GameObject selectedItem;
    public static Unit lastSelectedUnit;

    public enum Task { StandBy, PlayCard, MoveUnit, UnitMenuClicked, Transition }
    public static Task currentTask;

    public static IMouseController taskOwner;

    private static RaycastHit HitRay;
    private static Ray MouseRay;


    // private static GameObject _filter;
    private static GameObject HexMapGO;


    // Start is called before the first frame update
    void Start() {
        Selecteditems = new List<GameObject>();
        currentTask = Task.StandBy;
        // _filter = null;
    }

    // Update is called once per frame
    void Update() {

        if( Input.GetMouseButtonDown(0) ) {
            selectedItem = getClickedObject();
            leftClick();
        }else if (Input.GetMouseButtonDown(1)) {
            selectedItem = getClickedObject();
            rightClick();
        }


    }


    public static void leftClickObj(GameObject obj) {
        selectedItem = obj;
        leftClick();
    }

    private static void leftClick() {

        // If nothing is clicked
        if (selectedItem ==null ) {
            Debug.Log("Master Mouse, Clicked nothing");
            taskOwner.close();
            return;
        }

        /*
         * Makes sure that the item selected has a controller 
         */
        IMouseController clickedMouseController = selectedItem.GetComponent<IMouseController>();
        if (clickedMouseController == null) 
        {
            return;
        }
        else if (taskOwner == null) {
            //Debug.Log("task was null " + selectedItem.name);
            taskOwner = clickedMouseController;
            currentTask = clickedMouseController.GetTask();
            clickedMouseController.open();
        }
        else if (taskOwner != clickedMouseController) {
            //Debug.Log("clicked task  " + clickedMouseController.GetTask());
            clickedMouseController.open();
        }
        
        taskOwner.LeftClicked(selectedItem);


    }

    private void rightClick() {

        // Do nothing if nothing is selected and close if selected nothing
        if (taskOwner == null) {
            return;
        }else if (selectedItem == null) {
            taskOwner.close();
            return;
        }
        //Debug.Log("AAAAAA" + selectedItem.name);
        taskOwner.RightClicked(selectedItem);

    }

    public static void SetTask(Task task, IMouseController owner) {
        
        MasterMouse.taskOwner = owner;
        MasterMouse.currentTask = task;
    }


    private static GameObject getClickedObject() {
        MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        GameObject selected;
        UnitComponent selectedUnit;

        if (Physics.Raycast(MouseRay, out HitRay, 100f)) {
            //selectedItem = HitRay.transform.gameObject.transform.parent.transform.parent.gameObject;
            selected =  HitRay.transform.gameObject;
            selectedUnit = selected.GetComponent<UnitComponent>();
            if (selectedUnit) {
                lastSelectedUnit = selectedUnit.unit;
            }

            return selected;

        }

        return null;
    }


    

    


}
