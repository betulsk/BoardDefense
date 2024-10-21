using System;
using UnityEngine;

public class SimpleHealthProvider : MonoBehaviour, IHealthProvider
{
    private float _curHealth;

    [SerializeField] private float _maxHealth = 0;

    public Action<float> OnUpdated { get; set; }
    public Action OnDie;

    public float GetCurHealth()
    {
        return _curHealth;
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public float SetCurHealth(float health)
    {
        if(_curHealth == health)
        {
            return _curHealth;
        }
        _curHealth = health;
        OnUpdated?.Invoke(_curHealth);
        return _curHealth;
    }

    public void TakeDamage(int damage)
    {
        _curHealth -= damage;
        SetCurHealth(_curHealth);
        if(_curHealth < 0)
        {
            OnDie?.Invoke();
        }
    }

    public void Die()
    {
    }
}
