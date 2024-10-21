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
        List<EnemyData> enemyDatas = new List<EnemyData>();
        foreach(var item in GetActiveConfigData().ObjectTypeToEnemyData.Dictionary)
        {
            enemyDatas.Add(item.Value);
        }
        return enemyDatas;
    }

    public List<DefenseData> GetDefenseItemDataList()
    {
        var defenseDatas = new List<DefenseData>();
        foreach(var item in GetActiveConfigData().ObjectTypeToDefenseData.Dictionary)
        {
            defenseDatas.Add(item.Value);
        }
        return defenseDatas;
    }
    
    public EnemyData GetEnemyItemData(EPoolObjectType poolType)
    {
        EnemyData enemyData = new EnemyData();
        enemyData = GetActiveConfigData().ObjectTypeToEnemyData.Dictionary[poolType];
        return enemyData;
    }
    
    public DefenseData GetDefenseItemData(EPoolObjectType poolType)
    {
        DefenseData defenseData = new DefenseData();
        defenseData = GetActiveConfigData().ObjectTypeToDefenseData.Dictionary[poolType];
        return defenseData;
    }
}
