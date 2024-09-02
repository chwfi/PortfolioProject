using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class HealthComponent : BaseComponent, IDamageable
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    public bool Hit { get; private set; }

    private void Awake() 
    {
        _currentHealth = _maxHealth;
    }

    public void OnDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log(_currentHealth);

        Hit = true;

        if (_currentHealth <= 0)
        {
            Debug.Log("Dead");
        }

        HitCallback();
    }

    private void HitCallback()
    {
        CoroutineUtil.CallWaitForOneFrame(() => Hit = false);
    }
}
