using System.Collections.Generic;
using System;
using UnityEngine;

public class BoardPieceSpawner
{
    private GameConfig _gameConfig;
    private BoardPiece _boardPiecePrefab;

    private Dictionary<EPieceDirectionType, Vector2Int> _pieceDirectionTypeToCoordinates = new Dictionary<EPieceDirectionType, Vector2Int>()
    {
            { EPieceDirectionType.Up, new Vector2Int(0, 1) },
            { EPieceDirectionType.Right, new Vector2Int(1, 0) },
            { EPieceDirectionType.Down, new Vector2Int(0, -1) },
            { EPieceDirectionType.Left, new Vector2Int(-1, 0) },
    };

    public BoardPiece[,] _BoardPieces { get; private set; }
    public Dictionary<Vector2Int, BoardPiece> _PiecePositionToPiece { get; private set; }

    public BoardPieceSpawner(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
        _boardPiecePrefab = gameConfig.BoardPiecePrefab;
        _BoardPieces = new BoardPiece[_gameConfig.BoardSize.x, _gameConfig.BoardSize.y + 1];
        _PiecePositionToPiece = new Dictionary<Vector2Int, BoardPiece>();
    }

    public void Spawn(Transform parentTransform)
    {
        for(int i = 0; i < _BoardPieces.GetLength(1); i++)
        {
            for(int j = 0; j < _BoardPieces.GetLength(0); j++)
            {
                BoardPiece piece = UnityEngine.Object.Instantiate(_boardPiecePrefab, parentTransform);
                piece.SetPieceDatas(j, i, _gameConfig);
                _BoardPieces[j, i] = piece;

                if(i == _gameConfig.BoardSize.y)
                {
                    GameManager.Instance.EnemySpawnPositions.Add(piece);
                }

                _PiecePositionToPiece.Add(new Vector2Int(j, i), piece);
            }
        }
        SetNeighbours();
    }

    public List<BoardPiece> GetAttackableTiles(BoardPiece piece, EPieceDirectionType directionType, int range)
    {
        var currentPiece = piece;
        var attackablePieces = new List<BoardPiece>();
        var currentFoundPieceAmount = 0;
        while(currentFoundPieceAmount < range)
        {
            var pieceExist = currentPiece.Neighbours.TryGetValue(directionType, out var targetPiece);
            if(pieceExist)
            {
                attackablePieces.Add(targetPiece);
                currentFoundPieceAmount++;
                currentPiece = targetPiece;
            }
            else
            {
                break;
            }
        }

        return attackablePieces;
    }

    private void SetNeighbours()
    {
        Dictionary<EPieceDirectionType, BoardPiece> neighbours;
        var directionCount = Enum.GetValues(typeof(EPieceDirectionType)).Length;

        foreach(BoardPiece piece in _PiecePositionToPiece.Values)
        {
            neighbours = new Dictionary<EPieceDirectionType, BoardPiece>();

            for(int i = 0; i < directionCount; i++)
            {
                var possibleNeighbourCoordinates = piece.GridPosition + _pieceDirectionTypeToCoordinates[(EPieceDirectionType)i];
                if(_PiecePositionToPiece.TryGetValue(possibleNeighbourCoordinates, out var value))
                    neighbours.Add((EPieceDirectionType)i, value);

                piece.SetNeighbours(neighbours);
            }
        }
    }
}
