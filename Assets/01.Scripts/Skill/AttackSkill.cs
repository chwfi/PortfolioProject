using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

[CreateAssetMenu(menuName = "SO/Skill/AttackSkill", fileName = "Skill_")]
public class AttackSkill : Skill
{
    [Header("Stat")]
    [SerializeField] protected float _attackRadius;
    [SerializeField] protected int _attackDamage;
    [SerializeField] protected int _maxAttackableCount;

    [Header("Target Layer")]
    [SerializeField] private LayerMask _layer;

    public override void PlaySkill()
    {
        if (!Available) return;

        Collider2D[] result = new Collider2D[_maxAttackableCount];
        int count = Physics2D.OverlapCircleNonAlloc(_owner.AttackPos.position, _attackRadius, result, _layer);
        Debug.Log(count);

        for (int i = 0; i < count; i++)
        {
            var hit = result[i];
            if (hit.TryGetComponent(out IDamageable damageable))
            {
                damageable.OnDamage(_attackDamage, _owner.transform);
            }
        }

        Available = false;
        CoroutineUtil.CallWaitForSeconds(_coolDown, () => Available = true);
    }
}
