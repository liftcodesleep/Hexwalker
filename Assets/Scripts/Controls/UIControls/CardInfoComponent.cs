using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoComponent : MonoBehaviour
{
    GameObject _filter;
    GameObject cardInfo;
    // Start is called before the first frame update
    [SerializeField]
    MasterMouse mouse;
    [SerializeField]
    public TMPro.TMP_Text Text;
    [SerializeField]
    public TMPro.TMP_Text NameText;
    [SerializeField]
    public TMPro.TMP_Text HealthText;
    [SerializeField]
    public TMPro.TMP_Text StamanaText;
    [SerializeField]
    public TMPro.TMP_Text AttackText;
    [SerializeField]
    public TMPro.TMP_Text RangeText;
    [SerializeField]
    private RawImage HealthBar;
    [SerializeField]
    private RawImage StamanaBar;
    private UnitComponent UnitGO;

    void Start() {
        cardInfo = this.gameObject.transform.GetChild(0).gameObject;
        _filter = Game.GetFilter();
    }

    // Update is called once per frame
    void Update() {
        //if (_filter.activeSelf )
        if (MasterMouse.currentTask == MasterMouse.Task.MoveUnit || MasterMouse.currentTask == MasterMouse.Task.UnitMenuClicked) {
            cardInfo.gameObject.SetActive(true);
            UnitComponent newUnitGO = MasterMouse.selectedItem.GetComponent<UnitComponent>();

            if (newUnitGO != UnitGO) {
                if (UnitGO && UnitGO.CardCamera) {
                    UnitGO.CardCamera.enabled = false;
                }
                UnitGO = newUnitGO;
            }
            if (UnitGO) {
                NameText.text = UnitGO.unit.Name;
                AttackText.text = UnitGO.unit.Strength.ToString();
                RangeText.text = UnitGO.unit.Range.ToString();
                HealthText.text = UnitGO.unit.Health.ToString() + "/" + UnitGO.unit.HealthPoints.ToString();
                StamanaText.text = UnitGO.unit.ActionPoints.ToString() + "/" + UnitGO.unit.Actions.ToString();
                if (UnitGO.unit.HealthPoints == 0) {
                    HealthBar.transform.localScale = new Vector3(0, 1, 1);
                }
                else
                {
                    HealthBar.transform.localScale = new Vector3((float)UnitGO.unit.Health / (float)UnitGO.unit.HealthPoints, 1, 1);
                }

                if (UnitGO.unit.Actions == 0) {
                    StamanaBar.transform.localScale = new Vector3(0, 1, 1);
                }
                else
                {
                    StamanaBar.transform.localScale = new Vector3((float)UnitGO.unit.ActionPoints / (float)UnitGO.unit.Actions, 1, 1);
                }


                Text.text = UnitGO.unit.ToString();


                if (!UnitGO.CardCamera.enabled) {
                    UnitGO.CardCamera.enabled = true;
                }

            }


        }
        else //if(MasterMouse.currentTask == MasterMouse.Task.Transition) {

            cardInfo.gameObject.SetActive(false);
        if (UnitGO && UnitGO.CardCamera) {
            UnitGO.CardCamera.enabled = false;
        }
    }
}