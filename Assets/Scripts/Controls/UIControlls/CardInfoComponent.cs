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


    void Start()
    {
        cardInfo = this.gameObject.transform.GetChild(0).gameObject;
        _filter = Game.GetFilter();
    }

    // Update is called once per frame
    void Update()
    {
        if(_filter.activeSelf )
        {
            cardInfo.gameObject.SetActive(true);

            UnitComponent UnitGO = MasterMouse.selectedItem.GetComponent<UnitComponent>();

            if(UnitGO)
            {
                Text.text = UnitGO.unit.ToString();
            }


        }
        else if(!_filter.activeSelf )
        {
            cardInfo.gameObject.SetActive(false);
        }
        
    }
    
}
