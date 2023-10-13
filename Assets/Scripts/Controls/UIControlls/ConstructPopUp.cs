using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructPopUp : MonoBehaviour
{


    [SerializeField]
    GameObject effectSelectorGO;

    [SerializeField]
    GameObject popUp;

    private static UnitComponent UnitGO;

    // Start is called before the first frame update
    void Start()
    {
        //UnitComponent UnitGO = MasterMouse.selectedItem.GetComponent<UnitComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //if(UnitGO == null && MasterMouse.selectedItem != null)
        //{
        //    UnitGO = MasterMouse.selectedItem.GetComponent<UnitComponent>();
        //
        //    if(!UnitGO)
        //    {
        //        //popUp.SetActive(false);
        //        return;
        //    }
        //    else
        //    {
        //        popUp.SetActive(true);
        //    }
        //
        //}else if(UnitGO != null && MasterMouse.selectedItem != null)
        //{
        //    UnitComponent temp = MasterMouse.selectedItem.GetComponent<UnitComponent>();
        //    if (UnitGO == temp)
        //    {
        //        return;
        //    }
        //
        //    UnitGO = null;
        //}

    }


    public static void UseAbility()
    {
        MasterMouse.currentTask = MasterMouse.Task.UnitMenuClicked;

        Debug.Log("In pop up!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        


        Construct unit = MasterMouse.lastSelectedUnit;

        if (unit != null && unit.Abilities[0] != null)
        {
            //Effect.PutOnStack()
            unit.Abilities[0].ImmediateEffect();
        }

        MasterMouse.taskOwner.close();
        MasterMouse.SetTask(MasterMouse.Task.StandBy, null);
    }
}
