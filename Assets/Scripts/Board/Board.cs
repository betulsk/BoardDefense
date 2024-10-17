using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private BoardPieceSpawner _boardPieceSpawner;

    private void Start()
    {
        _boardPieceSpawner.Spawn();
    }
}
