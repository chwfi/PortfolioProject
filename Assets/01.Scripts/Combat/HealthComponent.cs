using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : BaseComponent, IDamageable
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    private void Awake() 
    {
        _currentHealth = _maxHealth;
    }

    public void OnDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log(_currentHealth);

        if (_currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }
}
