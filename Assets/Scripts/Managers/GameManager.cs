using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private int _currentLevelIndex; 

    private BoardPieceSpawner _boardPieceSpawner;
    public BoardPieceSpawner BoardPieceSpawner => _boardPieceSpawner;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _currentLevelIndex = PlayerData.LevelData;
        _boardPieceSpawner = new BoardPieceSpawner(GameConfigManager.Instance.GameConfigs[_currentLevelIndex]);
        _boardPieceSpawner.Spawn();
    }

}
