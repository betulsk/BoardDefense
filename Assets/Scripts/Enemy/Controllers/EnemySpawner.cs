using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<EPoolObjectType> _enemyDatas;
    private List<Enemy> _spawnedEnemies;
    private Coroutine _spawnRoutine;

    [SerializeField] private float _spawnDuration;

    private void Awake()
    {
        SetEnemies();
        GameManager.Instance.OnBoardCreated += OnBoardCreated;
        EventManager<OnEnemyDied>.SubscribeToEvent(OnEnemyDie);
    }

    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnBoardCreated -= OnBoardCreated;
        }
        EventManager<OnEnemyDied>.UnsubscribeToEvent(OnEnemyDie);

    }

    private void OnEnemyDie(object sender, OnEnemyDied dieEvent)
    {
        if(_spawnedEnemies.Contains(dieEvent.Enemy))
        {
            _spawnedEnemies.Remove(dieEvent.Enemy);
        }

        if(_enemyDatas.Count == 0 && _spawnedEnemies.Count == 0)
        {
            EventManager<OnLevelCompleted>.CustomAction(this, new OnLevelCompleted());
        }
    }

    private void SetEnemies()
    {
        _enemyDatas = new List<EPoolObjectType>();
        _spawnedEnemies = new List<Enemy>();

        foreach(var enemyData in GameConfigManager.Instance.GetEnemyDatas())
        {
            for(int i = 0; i < enemyData.EnemyCount; i++)
            {
                _enemyDatas.Add(enemyData.PoolType);
            }
        }
    }

    private void OnBoardCreated()
    {
        _enemyDatas.Shuffle();
        Spawn();
        _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        WaitForSeconds wfs = new WaitForSeconds(_spawnDuration);
        while(_enemyDatas.Count >= 0)
        {
            yield return wfs;
            Spawn();
        }
    }

    private void Spawn()
    {
        if(_enemyDatas.Count <= 0)
        {
            StopCoroutine(_spawnRoutine);
            return;
        }
        var resource = ResourcePoolManager.Instance.LoadResource(_enemyDatas[0], transform) as Enemy;
        _spawnedEnemies.Add(resource);
        _enemyDatas.RemoveAt(0);
    }
}
