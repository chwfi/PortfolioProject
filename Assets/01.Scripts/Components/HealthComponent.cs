using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class HealthComponent : BaseComponent, IDamageable
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    public Transform HitSubject { get; private set; } // 때린놈

    public bool Hit { get; private set; }
    public bool Undamagable { get; set; }
    public bool Dead { get; private set; }

    private void Awake() 
    {
        _currentHealth = _maxHealth;
    }

    public void OnDamage(int damage, Transform subject)
    {
        if (Undamagable) return;

        _currentHealth -= damage; // 들어온 데미지만큼 현재 체력 감소
        Debug.Log(_currentHealth);

        Hit = true;
        HitSubject = subject; // 때린 주체를 저장해놓음

        CoroutineUtil.Callback<bool>((Action) => Hit = false);

        if (_currentHealth <= 0)
        {
            Debug.Log("Dead");
            Dead = true;
        }
    }
}
