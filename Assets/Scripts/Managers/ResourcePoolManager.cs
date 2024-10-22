using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoolManager : Singleton<ResourcePoolManager>
{
    [SerializeField] private List<BaseResource> _poolObjects;
    [SerializeField] private int _minCount = 0;
    [SerializeField] private int _maxCount = Int32.MaxValue;

    public Dictionary<EPoolObjectType, Queue<BaseResource>> ResourceToGameObjectPool = new Dictionary<EPoolObjectType, Queue<BaseResource>>();
    public int MinCount => _minCount;
    public int MaxCount => _maxCount;

    private void Awake()
    {
        CreatePools();
    }

    private void CreatePools()
    {
        foreach (var poolObject in _poolObjects)
        {
            Queue<BaseResource> poolQueue = new Queue<BaseResource>();

            for(var i = 0; i< MaxCount; i++)
            {
                BaseResource resource = Instantiate(poolObject);
                resource.gameObject.SetActive(false);
                poolQueue.Enqueue(resource);
            }
            ResourceToGameObjectPool.Add(poolObject.PoolObjectType, poolQueue);
        }
    }

    public BaseResource LoadResource(EPoolObjectType poolObjectType, Transform initTransform)
    {
        if(!ResourceToGameObjectPool.ContainsKey(poolObjectType) || ResourceToGameObjectPool[poolObjectType].Count == 0)
            return null;

        var objectToSpawn = ResourceToGameObjectPool[poolObjectType].Dequeue();

        objectToSpawn.gameObject.SetActive(true);
        objectToSpawn.OnSpawnCustomAction(initTransform);
        return objectToSpawn;
    }

    public void ReturnPool(EPoolObjectType poolObjectType, BaseResource resource)
    {
        if(!ResourceToGameObjectPool.ContainsKey(poolObjectType))
            return;

        resource.gameObject.SetActive(false);
        ResourceToGameObjectPool[poolObjectType].Enqueue(resource);
    }
}
