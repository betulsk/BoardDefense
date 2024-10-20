using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    private EPieceType _pieceType = EPieceType.None;
    private Vector2Int _gridPosition;
    private Dictionary<EPieceDirectionType, BoardPiece> _neighbours;
    private bool _canPlaceItem;
    private bool _canPlaceResource;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Vector2Int GridPosition => _gridPosition;
    public Dictionary<EPieceDirectionType, BoardPiece> Neighbours => _neighbours;


    private BaseResource _resource;

    public BaseResource Resource
    {
        get { return _resource; }
        set { _resource = value; }
    }

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
        _canPlaceItem = columnY <= config.DefenceHeightThreshold;
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
