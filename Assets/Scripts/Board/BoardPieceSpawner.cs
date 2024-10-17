using UnityEngine;

public class BoardPieceSpawner
{
    private float _zValue = 0;
    private GameConfig _gameConfig;

    private BoardPiece _boardPiecePrefab;
    private Transform _pieceParentTransform;
    public BoardPiece[,] _BoardPieces { get; private set; }

    public BoardPieceSpawner(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
        _boardPiecePrefab = gameConfig.BoardPiecePrefab;
        _BoardPieces = new BoardPiece[_gameConfig.BoardSize.x, _gameConfig.BoardSize.y + 1];
    }

    public void Spawn()
    {
        for(int i = 0; i < _BoardPieces.GetLength(1); i++)
        {
            for(int j = 0; j < _BoardPieces.GetLength(0); j++)
            {
                BoardPiece piece = Object.Instantiate(_boardPiecePrefab, _pieceParentTransform);
                //_zValue += GameConfigManager.Instance.GetPieceOffset();
                piece.SetPieceDatas(j, i);
            }
        }
    }
}
