using System;
using UnityEngine;

public class Enemy : BaseResource, IHealthProvider
{
    [SerializeField] private EEnemyType _enemyType;
    [SerializeField] private EnemyMovementBehaviour _enemyMovementBehaviour;

    [SerializeField] private AutoShootBehaviour _autoShootBehaviour;
    [SerializeField] private EnemyVisualController _visualController;

    [SerializeField] private int _maxHealth = 0;

    #region Getter/Setter
    public EEnemyType EnemyType => _enemyType;
    public EnemyMovementBehaviour EnemyMovementBehaviour => _enemyMovementBehaviour;
    public AutoShootBehaviour AutoShootBehaviour => _autoShootBehaviour;

    public bool IsDead;
    public int Health;
    public int Damage;
    public int Interval;
    public float Speed;

    #endregion

    public Action<int> OHealthUpdated { get; set; }
    public Action OnSpawnAction;
    public Action OnDestroyAction;

    public override void OnSpawnCustomAction(Transform initTransform)
    {
        base.OnSpawnCustomAction(initTransform);
        int randomIndex = UnityEngine.Random.Range(0, GameManager.Instance.EnemySpawnPositions.Count);
        transform.position = GameManager.Instance.EnemySpawnPositions[randomIndex].transform.position;
        CurrentBoardPiece = GameManager.Instance.EnemySpawnPositions[randomIndex];
        SetDatas();
        CurrentBoardPiece.SetPieceElement(this);
        _enemyMovementBehaviour.TryMove();
        OnSpawnAction?.Invoke();
    }

    private void SetDatas()
    {
        var data = GameConfigManager.Instance.GetEnemyItemData(PoolObjectType);
        Health = data.Health;
        Damage = data.Damage;
        Interval = data.Interval;
        Speed = data.Speed;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public int GetCurHealth()
    {
        return Health;
    }

    public int SetCurHealth(int health)
    {
        if(Health == health)
        {
            return Health;
        }
        Health = health;
        OHealthUpdated?.Invoke(Health);
        return Health;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        SetCurHealth(Health);
        if(Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        IsDead = true;
        CurrentBoardPiece.CurrentResource = null;
        ResourcePoolManager.Instance.ReturnPool(PoolObjectType, this);
    }
}
