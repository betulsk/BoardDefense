using System;
using UnityEngine;

public class Enemy : BaseResource, IHealthProvider
{
    private Coroutine _waitRoutine;

    [SerializeField] private EnemyMovementBehaviour _enemyMovementBehaviour;
    [SerializeField] private AutoShootBehaviour _autoShootBehaviour;

    [SerializeField] private int _maxHealth = 0;
    [SerializeField] private float _waitDuration = 0.4f;

    #region Getter/Setter
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
        _maxHealth = data.Health;
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
        return Health;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        SetCurHealth(Health);
        OHealthUpdated?.Invoke(Health);
        if(Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        IsDead = true;
        CurrentBoardPiece.CurrentResource = null;
        if(_waitRoutine != null)
        {
            return;
        }
        _waitRoutine = StartCoroutine(this.WaitSecond(_waitDuration, () =>
        {
            ResourcePoolManager.Instance.ReturnPool(PoolObjectType, this);
            OnEnemyDied onEnemyDieEvent = new OnEnemyDied();
            onEnemyDieEvent.Enemy = this;
            EventManager<OnEnemyDied>.CustomAction(this, onEnemyDieEvent);
        }));
    }
}
