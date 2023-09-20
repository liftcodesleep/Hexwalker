using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Animation_Controller : MonoBehaviour, IUnitAnomator
{
    private Animator _animator;
    UnitComponent unitGO;

    public void AttackAnimation()
    {
        _animator.SetBool("attacked", true);
    }

    public void DeathAnimation()
    {
        throw new System.NotImplementedException();
    }

    public void IdleAnimation()
    {
        if(!_animator.GetBool("isMoving"))
        {
            return;
        }
        Debug.Log("Starting Idel animation");
        _animator.SetBool("isMoving", false);
       
    }

    public void MoveAnimation()
    {
        if (_animator.GetBool("isMoving"))
        {
            return;
        }
        Debug.Log("Starting run animation");
        _animator.SetBool("isMoving", true);
        
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

        unitGO = this.GetComponent<UnitComponent>();
        if(!unitGO)
        {
            throw new System.Exception("Animator is not attached to a unit");
        }


    }

    private void Update()
    {
        _animator.SetBool("attacked", false);

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
