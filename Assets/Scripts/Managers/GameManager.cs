using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class GameManager : Singleton<GameManager>
{
    private int _currentLevelIndex;
    private BoardPieceSpawner _boardPieceSpawner;

    [SerializeField] private Board _board;
    [SerializeField] private int _maxLevel;

    public BoardPieceSpawner BoardPieceSpawner => _boardPieceSpawner;
    public List<BoardPiece> EnemySpawnPositions;
    public int CurrentLevelIndex => _currentLevelIndex;
    public bool IsWin;
    public Action OnBoardCreated;

    private void Start()
    {
        Init();
        EventManager<OnLevelCompleted>.SubscribeToEvent(OnLevelComplete);
    }

    private void OnDestroy()
    {
        EventManager<OnLevelCompleted>.UnsubscribeToEvent(OnLevelComplete);
    }

    private void Init()
    {
        _currentLevelIndex = PlayerData.LevelData;
        Debug.Log("CurrentLevelData: " + _currentLevelIndex);
        _boardPieceSpawner = new BoardPieceSpawner(GameConfigManager.Instance.GameConfigs[_currentLevelIndex]);
        _boardPieceSpawner.Spawn(_board.PiecesVisualTransform);
        OnBoardCreated?.Invoke();
    }

    private void OnLevelComplete(object sender, OnLevelCompleted levelCompleteEvent)
    {
        if(levelCompleteEvent.IsWin)
        {
            PlayerData.LevelData++;
            Debug.Log("LevelDataArttırıldı " + PlayerData.LevelData);
            if(PlayerData.LevelData > _maxLevel)
            {
                PlayerData.LevelData = 0;
                Debug.Log("LevelDataSıfırlandı " + PlayerData.LevelData);
            }
        }
    }
}
