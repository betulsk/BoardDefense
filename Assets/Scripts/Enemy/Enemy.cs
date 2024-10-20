using UnityEngine;

public class Enemy : BaseResource
{
    public EEnemyType EnemyType;
    public float Speed;
    public float Health;

    public override void OnSpawnCustomAction()
    {
        base.OnSpawnCustomAction();
        int randomIndex = Random.Range(0, GameManager.Instance.EnemySpawnPositions.Count);
        transform.position = GameManager.Instance.EnemySpawnPositions[randomIndex].transform.position;
    }
}
