using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButtonControls : MonoBehaviour, IMouseController
{
    [SerializeField]
    private TMPro.TMP_Text nameText;

    private Effect _abilityEffect; 
    
    public void SetUp(Effect ability, int abilityNumber) {
        this._abilityEffect = ability;

        nameText.text = this._abilityEffect.Name;

        this.transform.position += Vector3.up * abilityNumber;
    }

    public void OnClick() {

        open();
        
    }


    private IEnumerator PlayEffect() {
        if(_abilityEffect.NumberOfTargets > 0) {
            while (_abilityEffect.NumberOfTargets < _abilityEffect.targets.Count) {
                yield return new WaitForSeconds(.01f);
            }
        }


        _abilityEffect.ImmediateEffect();

        //Effect.PutOnStack(_abilityEffect);

        MasterMouse.taskOwner.close();
        MasterMouse.SetTask(MasterMouse.Task.StandBy, null);

        //yield return new WaitForSeconds(.1f);
        
    }

    public MasterMouse.Task GetTask() {
        return MasterMouse.Task.UnitMenuClicked;
    }

    public void open() {
        MasterMouse.currentTask = MasterMouse.Task.UnitMenuClicked;
        MasterMouse.taskOwner.close();
        MasterMouse.taskOwner = this;

        StartCoroutine(PlayEffect());
    }

    public void LeftClicked(GameObject clickObject) {
        _abilityEffect.Target(clickObject);
    }

    public void RightClicked(GameObject clickObject) {
        close();
    }

    public void close() {
        Debug.Log("Closing button control");

        Game.map.UpdateVisible();
        MasterMouse.taskOwner = null;
        MasterMouse.currentTask = MasterMouse.Task.StandBy;
    }





}
