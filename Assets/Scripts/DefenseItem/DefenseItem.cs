using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefenseItem : BaseResource, IHealthProvider
{
    [SerializeField] private DefenseItemHealthController _healthController;
    [SerializeField] private TMP_Text _defenseItemText;

    public DefenseItemHealthController HealthController => _healthController;

    public Action<float> OnUpdated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public List<EPieceDirectionType> DirectionTypes;
    public EDefenseItemType DefenseType;
    public int Health;
    public int Damage;
    public int Interval;
    public int Range;

    public override void OnSpawnCustomAction(Transform initTransform)
    {
        base.OnSpawnCustomAction(initTransform);
        transform.position = initTransform.position;
        SetDatas();
    }

    private void SetDatas()
    {
        _defenseItemText.text = DefenseType.ToString();
        var data = GameConfigManager.Instance.GetDefenseItemData(PoolObjectType);
        Health = data.Health;
        Damage = data.Damage;
        Interval = data.Interval;
    }

    public int GetMaxHealth()
    {
        throw new NotImplementedException();
    }

    public int GetCurHealth()
    {
        throw new NotImplementedException();
    }

    public int SetCurHealth(int health)
    {
        if(Health == health)
        {
            return Health;
        }
        Health = health;
        OnUpdated?.Invoke(Health);
        return Health;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage");
    }

    public void Die()
    {
        Debug.Log("Die");

    }
}
