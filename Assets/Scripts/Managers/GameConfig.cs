using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig",
   menuName = "GameConfig/Create a GameConfig",
   order = 1)]
public class GameConfig : ScriptableObject
{
    [Header("BOARD")]
    [SerializeField] private Vector2Int _boardSize;
    [SerializeField] private BoardPiece _boardPiece;
    [SerializeField] private int _defenceHeightThreshold;

    [Header("ENEMY")]
    [SerializeField] private List<EnemyData> _enemyDatas;
    public SerializableDictionary<EPoolObjectType, EnemyData> ObjectTypeToEnemyData;

    [Header("ITEM")]
    [SerializeField] private List<DefenseData> _defenseData;
    public SerializableDictionary<EPoolObjectType, DefenseData> ObjectTypeToDefenseData;

    public Vector2Int BoardSize => _boardSize;
    public BoardPiece BoardPiecePrefab => _boardPiece;
    public int DefenceHeightThreshold => _defenceHeightThreshold;
    public Color ActivePieceColor;
    public Color PassivePieceColor;
    public float EnemySpawnDuration;
}

[System.Serializable]
public struct EnemyData
{
    public EPoolObjectType PoolType;
    public Enemy EnemyPrefab;
    public int EnemyCount;
    public int Health;
    public int Damage;
    public int Interval;
    public float Speed;
}

[System.Serializable]
public struct DefenseData
{
    public EPoolObjectType PoolType;
    public DefenseItem DefenseItemPrefab;
    public List<EPieceDirectionType> DirectionTypes;
    public int ItemCount;
    public int Health;
    public int Damage;
    public int Interval;
    public int Range;
}
