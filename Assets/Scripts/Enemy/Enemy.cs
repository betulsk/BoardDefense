using System;
using UnityEngine;

public class Enemy : BaseResource
{
    [SerializeField] private EEnemyType _enemyType;
    [SerializeField] private EnemyMovementBehaviour _enemyMovementBehaviour;
    [SerializeField] private AutoShootBehaviour _autoShootBehaviour;

    #region Getter/Setter
    public EEnemyType EnemyType => _enemyType;
    public EnemyMovementBehaviour EnemyMovementBehaviour => _enemyMovementBehaviour;
    public AutoShootBehaviour AutoShootBehaviour => _autoShootBehaviour;

    public bool IsDead;
    public int Health;
    public int Damage;
    public int Interval;
    public float Speed;

    public Action OnSpawnAction;
    public Action OnDestroyAction;
    #endregion

    public override void OnSpawnCustomAction(Transform initTransform)
    {
        base.OnSpawnCustomAction(initTransform);
        int randomIndex = UnityEngine.Random.Range(0, GameManager.Instance.EnemySpawnPositions.Count);
        transform.position = GameManager.Instance.EnemySpawnPositions[randomIndex].transform.position;
        CurrentBoardPiece = GameManager.Instance.EnemySpawnPositions[randomIndex];
        SetDatas();
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
}
