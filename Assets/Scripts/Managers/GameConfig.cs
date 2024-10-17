using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig",
   menuName = "GameConfig/Create a GameConfig",
   order = 1)]
public class GameConfig : ScriptableObject
{
    [Header("BOARD")]
    [SerializeField] private Vector2Int _boardSize;
    public Vector2Int BoardSize => _boardSize;

    [SerializeField] private float _pieceDistance;
    public float PieceOffset => _pieceDistance;

    [SerializeField] private BoardPiece _boardPiece;
    public BoardPiece BoardPiecePrefab => _boardPiece;
}
