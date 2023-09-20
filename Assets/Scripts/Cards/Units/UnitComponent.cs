using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitComponent : MonoBehaviour
{

    public Unit unit;

    private Vector3 oldPostion;
    private Vector3 newPosition;
    public Vector3 currentVelocity;
    private IUnitAnomator anomator;
    //
    float smoothTime = .5f;
    //
    //
    //private float shrinkSpeed = .9f;
    //private int oldHealth;
    //
    private void Start()
    {
    
        oldPostion = newPosition = this.transform.position;
        //this.transform.localScale = new Vector3(.01f, .01f, .01f);
        anomator = this.GetComponent<IUnitAnomator>();

        if(anomator == null)
        {
            throw new System.Exception(unit.Name + " Has no animations");
        }

        //this.oldHealth = unit.HealthPoints;
    }
    //
    private void Update()
    {
        //if (unit.HealthPoints <= 0)
        //{
        //    HandleDeath();
        //    return;
        //}
        //else if (this.transform.localScale.x < 1)
        //{
        //    this.transform.localScale += new Vector3(.01f, .01f, .01f);
        //}
    
        //if (this.oldHealth != unit.HealthPoints)
        //{
        //    HandleHit();
        //}
        UpdatePosition();
        UpdateHexPosition();
    }
    //
    private void UpdateHexPosition()
    {
        HexComponent parentComponent = this.transform.parent.GetComponent<HexComponent>();
        if (parentComponent == null)
        {
            Debug.Log("Ooops");
            return;
        }
        Hex parentHex = parentComponent.hex;
        if (this.unit.Location != parentHex)
        {
            
            HexComponent newComponent = Game.map.GetHexGO(this.unit.Location).GetComponent<HexComponent>();
            newPosition = newComponent.PositionFromCamera();
            currentVelocity = Vector3.zero;
            this.transform.parent = newComponent.transform;
        }else
        {
            
        }
    }
    //
    //public void OnUnitMove(Hex newHex)
    //{
    //    
    //    if (unit.Move(newHex))
    //    {
    //        HexComponent newComponent = Game.map.GetHexGO(newHex).GetComponent<HexComponent>();
    //        newPosition = newComponent.transform.position;
    //        currentVelocity = Vector3.zero;
    //        this.transform.parent = newComponent.transform;
    //    }
    //}
    //
    //
    //
    //private void HandleDeath()
    //{
    //    
    //    if (this.gameObject.transform.localScale.magnitude < .1f)
    //    {
    //        
    //
    //        unit.OnDeath();
    //        Destroy(this.gameObject);
    //
    //    }
    //    else
    //    {
    //        this.gameObject.transform.localScale *= shrinkSpeed;
    //    }
    //}
    //
    //private void HandleHit()
    //{
    //    
    //    this.oldHealth = unit.HealthPoints;
    //
    //}
    //
    //public void HandleAttack()
    //{
    //    
    //}
    //
    private void UpdatePosition()
    {
        if (newPosition == oldPostion)
        {
            anomator.IdleAnimation();
            
        }
        else if (FinishedMove())
        {
            anomator.IdleAnimation();
            oldPostion = newPosition;
        }
        else
        {
            anomator.MoveAnimation();
            Debug.Log("Calling move");
            this.transform.position = Vector3.SmoothDamp(this.transform.position, newPosition, ref currentVelocity, smoothTime);
            Vector3 lookDirection = newPosition - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = rotation;
        }
    }
    //
    private bool FinishedMove()
    {
        
        return (newPosition - this.transform.position).magnitude < .1f;
    }
    //
    //
}
