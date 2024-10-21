using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardPiece : MonoBehaviour, IPointerClickHandler
{
    private EPieceType _pieceType = EPieceType.None;
    private Vector2Int _gridPosition;
    private Dictionary<EPieceDirectionType, BoardPiece> _neighbours;

    private BaseResource _resource;
    private bool _canPlaceResource;

    [SerializeField] private PieceVisualController _visualController;

    public Vector2Int GridPosition => _gridPosition;

    public Dictionary<EPieceDirectionType, BoardPiece> Neighbours => _neighbours;

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

    public bool IsPlacablePiece;

    private void OnEnable()
    {
        EventManager<ButtonClickEvent>.SubscribeToEvent(OnButtonClicked);
    }

    private void OnDisable()
    {
        EventManager<ButtonClickEvent>.UnsubscribeToEvent(OnButtonClicked);
    }

    public void SetPieceDatas(int rowX, int columnY, GameConfig config)
    {
        _gridPosition = new Vector2Int(rowX, columnY);
        transform.position = new Vector3(rowX, columnY, transform.position.z);
        PieceType = EPieceType.Empty;
        Resource = null;
        IsPlacablePiece = columnY <= config.DefenceHeightThreshold;
    }

    private void OnButtonClicked(object sender, ButtonClickEvent @event)
    {
        _visualController.TryChangePieceColor();
    }

    public void SetNeighbours(Dictionary<EPieceDirectionType, BoardPiece> neighbours)
    {
        _neighbours = neighbours;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        if(TryPlaceItem())
        {
            PlaceItem();
        }
    }

    private bool TryPlaceItem()
    {
        if (Resource != null || !IsPlacablePiece || PieceType != EPieceType.Empty)
        {
            return false;
        }
        return true;
    }

    private void PlaceItem()
    {
        Debug.Log("Defence Item placed");
    }
}
