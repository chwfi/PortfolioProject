using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : TargetComponent
{
    public override Entity GetTarget()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, _detectRange, _targetLayer);
        coll.TryGetComponent(out Entity entity);
        return entity;
    }

    public override bool Targeting()
    {
        return Physics2D.OverlapCircle(transform.position, _detectRange, _targetLayer);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectRange);
    }
}
