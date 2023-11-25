using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfoComponent : MonoBehaviour
{
    GameObject _filter;
    GameObject cardInfo;
    
    // Start is called before the first frame update

    [SerializeField]
    MasterMouse mouse;

    [SerializeField]
    public TMPro.TMP_Text Text;

    private UnitComponent UnitGO;


    void Start()
    {
        cardInfo = this.gameObject.transform.GetChild(0).gameObject;
        
        _filter = Game.GetFilter();
    }

    // Update is called once per frame
    void Update()
    {

        //if (_filter.activeSelf )
        if (MasterMouse.currentTask == MasterMouse.Task.MoveUnit || MasterMouse.currentTask == MasterMouse.Task.UnitMenuClicked)
        {
            cardInfo.gameObject.SetActive(true);
            UnitComponent newUnitGO = MasterMouse.selectedItem.GetComponent<UnitComponent>();

            if(newUnitGO != UnitGO)
            {
                if (UnitGO && UnitGO.CardCamera)
                {
                    UnitGO.CardCamera.enabled = false;
                    

                }
                UnitGO = newUnitGO;
            }

            if(UnitGO)
            {
                Text.text = UnitGO.unit.ToString();
                if(!UnitGO.CardCamera.enabled)
                {
                    UnitGO.CardCamera.enabled = true;
                }
                
            }


        }
        else //if(MasterMouse.currentTask == MasterMouse.Task.Transition)
        {
            
            cardInfo.gameObject.SetActive(false);
            if (UnitGO && UnitGO.CardCamera)
            {
                UnitGO.CardCamera.enabled = false;
            }
        }
        
    }
    
}
