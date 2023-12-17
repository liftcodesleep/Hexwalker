using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackEffect : Effect
{
    Unit attackingUnit;
    Unit targetUnit;
    UnitComponent attacker;
    UnitComponent target;
    public AttackEffect(Unit attackerUnit,Unit targetUnit, UnitComponent attackerGO, UnitComponent targetGO ) : base(attackerUnit)
    {

        base.Desctiption = attacker.name + " is attacking " + attackerUnit.Name;
        attackingUnit = attackerUnit;
        this.targetUnit = targetUnit;
        this.attacker = attackerGO;
        this.target = targetGO;
    }

    public override void EndTurnEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void ImmediateEffect()
    {
        target.GetComponentInChildren<Knight_Animation_Controller>().TakeDamageAnimation();
        attacker.GetComponentInChildren<Knight_Animation_Controller>().AttackAnimation();

        target.transform.LookAt(attacker.transform.position);
        attacker.transform.LookAt(target.transform.position);

        this.attackingUnit.Attack(targetUnit);
    }

    public override void Target(GameObject target)
    {
        throw new System.NotImplementedException();
    }

    public override void TargetedEffect(Type typeTargeted)
    {
        throw new System.NotImplementedException();
    }
}
