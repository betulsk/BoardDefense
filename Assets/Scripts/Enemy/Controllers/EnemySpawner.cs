using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<EPoolObjectType> _enemyDatas;
    private Coroutine _spawnRoutine;

    [SerializeField] private float _spawnDuration;

    private void Awake()
    {
        SetEnemies();
        GameManager.Instance.OnBoardCreated += OnBoardCreated;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnBoardCreated -= OnBoardCreated;
        }
    }

    private void SetEnemies()
    {
        _enemyDatas = new List<EPoolObjectType>();
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
        ResourcePoolManager.Instance.LoadResource(_enemyDatas[0], transform);
        _enemyDatas.RemoveAt(0);
    }
}
