using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ConstructPopUp : MonoBehaviour
{


    [SerializeField]
    GameObject effectSelectorGO;

    [SerializeField]
    GameObject popUpPanel;

    [SerializeField]
    GameObject abilityButtonPrefab;

    Construct currentUnit;



    private static UnitComponent UnitGO;

    // Start is called before the first frame update
    void Start()
    {
        //UnitComponent UnitGO = MasterMouse.selectedItem.GetComponent<UnitComponent>();
        currentUnit = null;

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


        UpdateAbilities();

    }

    private void removeOldAbilities()
    {

        if(popUpPanel.transform.childCount == 0)
        {
            return;
        }

        GameObject currentChild = popUpPanel.transform.GetChild(0).gameObject;

        while ( currentChild != null)
        {
            GameObject.Destroy(currentChild);
            currentChild = popUpPanel.transform.GetChild(0).gameObject;
            break;
        }
        
    }
    private void addAbilityButtons()
    {
        GameObject currentAbilityButton;
        int ability_index = 0;
        if(currentUnit == null) { return; }
        if(currentUnit.Abilities == null || currentUnit.Abilities.Count == 0 ) { return; }
        foreach(Effect currentAbility in currentUnit.Abilities)
        {
            currentAbilityButton = Instantiate(abilityButtonPrefab, popUpPanel.transform);
            currentAbilityButton.GetComponent<AbilityButtonControlls>().SetUp(currentAbility, ability_index);
            ability_index++;
        }
    }
    private void UpdateAbilities()
    {
        if( MasterMouse.lastSelectedUnit == currentUnit && MasterMouse.lastSelectedUnit != null )
        {
            return;
        }

        removeOldAbilities();
        currentUnit = MasterMouse.lastSelectedUnit;
        addAbilityButtons();

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
