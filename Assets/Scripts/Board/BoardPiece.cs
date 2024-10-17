using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    private EPieceType _pieceType = EPieceType.None;
    private bool _canPlace;
    private Vector2Int _gridPosition;
    private Dictionary<EPieceDirectionType, BoardPiece> _neighbours;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Vector2Int GridPosition => _gridPosition;
    public Dictionary<EPieceDirectionType, BoardPiece> Neighbours => _neighbours;

    public EPieceType PieceType
    {
        get { return _pieceType; }
        set { _pieceType = value; }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void SetPieceDatas(int rowX, int columnY, GameConfig config)
    {
        _gridPosition = new Vector2Int(rowX, columnY);
        transform.position = new Vector3(rowX, columnY, transform.position.z);
        PieceType = EPieceType.Empty;
        _canPlace = columnY <= config.DefenceHeightThreshold;
    }

    private void ChangePieceColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    public void SetNeighbours(Dictionary<EPieceDirectionType, BoardPiece> neighbours)
    {
        _neighbours = neighbours;
    }
}
