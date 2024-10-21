using UnityEngine;

public class Enemy : BaseResource
{
    [SerializeField] private EEnemyType _enemyType;
    [SerializeField] private EnemyMovementBehaviour _enemyMovementBehaviour;

    #region Getter/Setter
    public EEnemyType EnemyType => _enemyType;
    public EnemyMovementBehaviour EnemyMovementBehaviour => _enemyMovementBehaviour;

    public float MovementSpeed;
    public float Health;
    #endregion

    public override void OnSpawnCustomAction(Transform initTransform)
    {
        base.OnSpawnCustomAction(initTransform);
        int randomIndex = Random.Range(0, GameManager.Instance.EnemySpawnPositions.Count);
        transform.position = GameManager.Instance.EnemySpawnPositions[randomIndex].transform.position;
        CurrentBoardPiece = GameManager.Instance.EnemySpawnPositions[randomIndex];
        _enemyMovementBehaviour.StartMovement();
    }
}
