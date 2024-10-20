using UnityEngine;

[System.Serializable]
public class PoolObject
{
    [SerializeField] private EPoolObjectType _poolObjectType;
    [SerializeField] private BaseResource _objectPrefab;
    [SerializeField] private int _poolSize;

    public EPoolObjectType PoolObjectType => _poolObjectType;
    public BaseResource ObjectPrefab => _objectPrefab;
    public int PoolSize => _poolSize;
}
