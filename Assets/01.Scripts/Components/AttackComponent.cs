using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackComponent : BaseComponent
{
    [Header("Transform")]
    [SerializeField] protected Transform _attackPos;

    [Header("Stat")]
    [SerializeField] protected float _attackRadius;
    [SerializeField] protected int _attackDamage;
    [SerializeField] protected int _maxAttackEnemyCount;

    public abstract void OnAttack();
}
