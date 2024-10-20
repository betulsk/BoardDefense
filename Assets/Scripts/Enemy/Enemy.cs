using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseResource
{
    public EEnemyType EnemyType;
    public float Speed;
    public float Health;

    public override void OnSpawnCustomAction()
    {
        base.OnSpawnCustomAction();
        transform.position = GameManager.Instance.EnemySpawnPositions[0].transform.position;
    }
}
