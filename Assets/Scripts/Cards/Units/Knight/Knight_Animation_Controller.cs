using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Animation_Controller : MonoBehaviour, IUnitAnomator
{
    private Animator _animator;
    UnitComponent unitGO;

    [SerializeField]
    private AudioSource runSound;

    public void AttackAnimation()
    {
        
        StartCoroutine(attack());
        
    }
    IEnumerator attack()
    {
        Debug.Log("Aniimation Attack");
        _animator.SetBool("attacked", true);
        yield return new WaitForSeconds(.1f);
        _animator.SetBool("attacked", false);

    }
    public void DeathAnimation()
    {
        throw new System.NotImplementedException();
    }

    public void IdleAnimation()
    {
        
        if (!_animator.GetBool("Moving"))
        {
            return;
        }
        Debug.Log("Starting Idel animation");
        _animator.SetBool("Moving", false);
       
    }

    public void MoveAnimation()
    {
        
        if (_animator.GetBool("Moving"))
        {
            return;
        }
        Debug.Log("Starting run animation");
        _animator.SetBool("Moving", true);
        runSound.Play();
    }

    public void TakeDamageAnimation()
    {
        throw new System.NotImplementedException();
    }

    public void UseSpellAnimation()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _animator.applyRootMotion = false;

        unitGO = this.transform.parent.gameObject.GetComponent<UnitComponent>();
        if(!unitGO)
        {
            throw new System.Exception("Animator is not attached to a unit");
        }


    }

    private void Update()
    {
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
