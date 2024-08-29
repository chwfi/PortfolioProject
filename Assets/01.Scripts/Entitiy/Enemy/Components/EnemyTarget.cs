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
        // 해당 위치에서 _detectRange 내에 있는 충돌체를 확인한다
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _detectRange, _targetLayer);
        
        if (collider == null) // 충돌체가 없으면 false를 반환
            return false;

        // 있다면 거리 계산
        float distance = Vector2.Distance(transform.position, collider.transform.position);

        // 최소/최대 범위 사이에 있다면 true를 반환
        return distance >= _stopRange && distance <= _detectRange;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectRange);
        Gizmos.DrawWireSphere(transform.position, _stopRange);
    }
}
