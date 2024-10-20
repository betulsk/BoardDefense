using TMPro;
using UnityEngine;

public abstract class BaseResource : MonoBehaviour
{
    [SerializeField] private EPoolObjectType _poolObjectType;
    [SerializeField] private TMP_Text _sourceText;

    public EPoolObjectType PoolObjectType => _poolObjectType;

    public virtual void OnSpawnCustomAction()
    {
    }
}
