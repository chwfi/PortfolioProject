using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackComponent
{
    [Header("Target Layer")]
    [SerializeField] private LayerMask _enemyLayer;

    public override void OnAttack()
    {
        Collider2D[] result = new Collider2D[_maxAttackEnemyCount];
        int count = Physics2D.OverlapCircleNonAlloc(_attackPos.position, _attackRadius, result, _enemyLayer);
        Debug.Log(count);

        for (int i = 0; i < count; i++)
        {
            var hit = result[i];
            if (hit.TryGetComponent(out IDamageable damageable))
            {
                damageable.OnDamage(_attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPos.position, _attackRadius);
    }
}
