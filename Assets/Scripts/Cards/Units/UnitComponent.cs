using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitComponent : MonoBehaviour
{

    public Unit unit;
    public Camera CardCamera;

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
    private Vector3 originalScale;
    private void Start()
    {
        originalScale = this.transform.GetChild(0).transform.localScale;
        this.transform.GetChild(0).transform.localScale = new Vector3(.01f, .01f, .01f);

        oldPostion = newPosition = this.transform.position;
        
        anomator = this.GetComponentInChildren<IUnitAnomator>();

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
        if (this.transform.GetChild(0).transform.localScale.x < originalScale.x)
        {
            this.transform.GetChild(0).transform.localScale += new Vector3(.01f, .01f, .01f);
        }
    
        //if (this.oldHealth != unit.HealthPoints)
        //{
        //    HandleHit();
        //}
        UpdatePosition();
        UpdateHexPosition();

        HandleDeath();

    }

    private void HandleDeath()
    {
        if(unit.currentZone == CardZone.Types.GraveYard)
        {
            this.transform.localScale *= .9f;
        }

        if(this.transform.localScale.magnitude < .1f)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    //
    private void UpdateHexPosition()
    {
        if(this.unit.Location == null) { return; }
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
    public void HandleAttack()
    {
        anomator.AttackAnimation();
        
    }
    
    private void UpdatePosition()
    {
        if (newPosition == oldPostion)
        {
            if(anomator != null)
            {
                anomator.IdleAnimation();
            }
            
            
        }
        else if (FinishedMove())
        {
            if (anomator != null)
            {
                anomator.IdleAnimation();
            }
            oldPostion = newPosition;
        }
        else
        {
            if (anomator != null)
            {
                anomator.MoveAnimation();
            }
            
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
