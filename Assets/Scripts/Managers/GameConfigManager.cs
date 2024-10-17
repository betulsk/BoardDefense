using System.Collections.Generic;
using UnityEngine;

public class GameConfigManager : Singleton<GameConfigManager>
{
    [SerializeField] private List<GameConfig> _gameConfigs;
    public List<GameConfig> GameConfigs => _gameConfigs;
}
