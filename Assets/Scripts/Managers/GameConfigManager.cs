using System.Collections.Generic;
using UnityEngine;

public class GameConfigManager : Singleton<GameConfigManager>
{
    [SerializeField] private List<GameConfig> _gameConfigs;
    public List<GameConfig> GameConfigs => _gameConfigs;

    public List<EnemyData> GetEnemyDatas()
    {
        return _gameConfigs[GameManager.Instance.CurrentLevelIndex].EnemyDatas;
    }

    public List<DefenseData> GetDefenseItemData()
    {
        return _gameConfigs[GameManager.Instance.CurrentLevelIndex].DefenseData;
    }
}
