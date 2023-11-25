using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Animation_Controller : MonoBehaviour, IUnitAnomator
{
    private Animator _animator;
    UnitComponent unitGO;

    [SerializeField]
    private AudioSource runSound;

    [SerializeField]
    private AudioSource attackSound;

    [SerializeField]
    private AudioSource summonSound;


    [SerializeField]
    private AudioSource[] runSoundArray;

    [SerializeField]
    private AudioSource[] attackSoundArray;

    [SerializeField]
    private AudioSource[] summonSoundArray;



    public void AttackAnimation() {
        
        StartCoroutine(attack());
        
    }
    IEnumerator attack() {
        //Debug.Log("Aniimation Attack");
        _animator.SetBool("attacked", true);
        yield return new WaitForSeconds(.1f);
        _animator.SetBool("attacked", false);

    }
    public void DeathAnimation() {
        StartCoroutine(deathAnimationplay());

    }

    IEnumerator deathAnimationplay() {
        //Debug.Log("Aniimation Attack");
        _animator.SetBool("attacked", true);
        yield return new WaitForSeconds(.1f);
        _animator.SetBool("attacked", false);

    }

    public void IdleAnimation() {
        
        if (!_animator.GetBool("Moving")) {
            return;
        }
        Debug.Log("Starting Idel animation");
        _animator.SetBool("Moving", false);
       
    }

    public void MoveAnimation() {
        
        if (_animator.GetBool("Moving")) {
            return;
        }
        Debug.Log("Starting run animation");
        _animator.SetBool("Moving", true);
        if(runSound == null) {
            return;
        }
        runSound.Play();
    }

    public void TakeDamageAnimation() {
        StartCoroutine(damageAnimationplay());
    }

    IEnumerator damageAnimationplay() {
        Debug.Log("Aniimation Damage!!!!!!!!!!!!!");
        _animator.SetBool("damaged", true);
        yield return new WaitForSeconds(1f);
        _animator.SetBool("damaged", false);

    }

    public void UseSpellAnimation() {
        throw new System.NotImplementedException();
    }

    private void Start() {
        _animator = GetComponent<Animator>();

        _animator.applyRootMotion = false;

        unitGO = this.transform.parent.gameObject.GetComponent<UnitComponent>();
        if(!unitGO) {
            throw new System.Exception("Animator is not attached to a unit");
        }


    }

    private void Update() {
        //_animator.SetBool("attacked", false);

        //if(unitGO.currentVelocity.magnitude > 1)
        //{
        //    MoveAnimation();
        //}
        //else
        //{
        //    IdleAnimation();
        //}
        

    }
}
