using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig",
   menuName = "GameConfig/Create a GameConfig",
   order = 1)]
public class GameConfig : ScriptableObject
{
    [Header("BOARD")]
    [SerializeField] private Vector2Int _boardSize;
    public Vector2Int BoardSize => _boardSize;

    [SerializeField] private float _pieceDistance;
    public float PieceOffset => _pieceDistance;

    [SerializeField] private BoardPiece _boardPiece;
    public BoardPiece BoardPiecePrefab => _boardPiece;

    [SerializeField] private int _defenceHeightThreshold;
    public int DefenceHeightThreshold => _defenceHeightThreshold;

    public Color ActivePieceColor;
    public Color PassivePieceColor;

    [Header("ENEMY")]
    [SerializeField] private List<EnemyData> _enemyDatas;
    public List<EnemyData> EnemyDatas => _enemyDatas;
    public float EnemySpawnDuration;

    [Header("ITEM")]
    [SerializeField] private List<DefenseData> _defenseData;
    public List<DefenseData> DefenseData => _defenseData;

}

[System.Serializable]
public struct EnemyData
{
    public EPoolObjectType PoolType;
    public Enemy EnemyPrefab;
    public int EnemyCount;
}

[System.Serializable]
public struct DefenseData
{
    public EPoolObjectType PoolType;
    public DefenseItem DefenseItemPrefab;
    public List<EPieceDirectionType> DirectionTypes;
    public int ItemCount;
    public int Damage;
    public int Range;
    public int Interval;
}
