using System.Collections.Generic;
using UnityEngine;

public class GameConfigManager : Singleton<GameConfigManager>
{
    [SerializeField] private List<GameConfig> _gameConfigs;
    public List<GameConfig> GameConfigs => _gameConfigs;

    public GameConfig GetActiveConfigData()
    {
        return GameConfigs[GameManager.Instance.CurrentLevelIndex];
    }

    public List<EnemyData> GetEnemyDatas()
    {
        return GetActiveConfigData().EnemyDatas;
    }

    public List<DefenseData> GetDefenseItemData()
    {
        var defenseDatas = new List<DefenseData>();
        foreach(var item in GetActiveConfigData().ObjectTypeToDefenseData.Dictionary)
        {
            defenseDatas.Add(item.Value);
        }
        return defenseDatas;
    }
}
