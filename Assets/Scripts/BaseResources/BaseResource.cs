using TMPro;
using UnityEngine;

public abstract class BaseResource : MonoBehaviour
{
    [SerializeField] private EPoolObjectType _poolObjectType;
    [SerializeField] private TMP_Text _sourceText;
    [SerializeField] private BoardPiece _currentBoardPiece;

    public EPoolObjectType PoolObjectType => _poolObjectType;

    public BoardPiece CurrentBoardPiece
    {
        get { return _currentBoardPiece; }
        set { _currentBoardPiece = value; }
    }


    public virtual void OnSpawnCustomAction()
    {
    }
}
