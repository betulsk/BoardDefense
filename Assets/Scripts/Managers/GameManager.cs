using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class GameManager : Singleton<GameManager>
{
    private int _currentLevelIndex; 

    private BoardPieceSpawner _boardPieceSpawner;

    [SerializeField] private Board _board;
    public BoardPieceSpawner BoardPieceSpawner => _boardPieceSpawner;
    public List<BoardPiece> EnemySpawnPositions;
    public int CurrentLevelIndex => _currentLevelIndex;

    public Action OnBoardCreated;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _currentLevelIndex = PlayerData.LevelData;
        _boardPieceSpawner = new BoardPieceSpawner(GameConfigManager.Instance.GameConfigs[_currentLevelIndex]);
        _boardPieceSpawner.Spawn(_board.PiecesVisualTransform);
        OnBoardCreated?.Invoke();
    }
}
