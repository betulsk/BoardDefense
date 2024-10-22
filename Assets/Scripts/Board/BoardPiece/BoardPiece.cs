using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardPiece : MonoBehaviour, IPointerClickHandler
{
    private EPoolObjectType _poolType;
    private Vector2Int _gridPosition;
    private Dictionary<EPieceDirectionType, BoardPiece> _neighbours;
    private BaseResource _resource;
    private EPoolObjectType _clickedButtonType;

    [SerializeField] private PieceVisualController _visualController;

    public Vector2Int GridPosition => _gridPosition;

    public Dictionary<EPieceDirectionType, BoardPiece> Neighbours => _neighbours;

    public BaseResource CurrentResource
    {
        get { return _resource; }
        set { _resource = value; }
    }

    public EPoolObjectType PoolType
    {
        get { return _poolType; }
        set { _poolType = value; }
    }

    public bool IsPlacablePiece;
    public bool IsItemSelected;

    private void OnEnable()
    {
        EventManager<ButtonClickEvent>.SubscribeToEvent(OnButtonClicked);
        EventManager<OnDefenceItemPlaced>.SubscribeToEvent(OnDefenceItemPlace);
    }

    private void OnDisable()
    {
        EventManager<ButtonClickEvent>.UnsubscribeToEvent(OnButtonClicked);
        EventManager<OnDefenceItemPlaced>.UnsubscribeToEvent(OnDefenceItemPlace);
    }

    public void SetPieceDatas(int rowX, int columnY, GameConfig config)
    {
        _gridPosition = new Vector2Int(rowX, columnY);
        transform.position = new Vector3(rowX, columnY, transform.position.z);
        CurrentResource = null;
        IsPlacablePiece = columnY <= config.DefenceHeightThreshold;
    }

    public void SetNeighbours(Dictionary<EPieceDirectionType, BoardPiece> neighbours)
    {
        _neighbours = neighbours;
    }

    public void SetPieceElement(BaseResource tileElement)
    {
        CurrentResource = tileElement;
        CurrentResource.transform.parent = transform;
        CurrentResource.SetPiece(this);

        if(CurrentResource is DefenseItem defenceItem)
        {
            OnDefenceItemPlaced onDefenseItemPlaced = new OnDefenceItemPlaced();
            onDefenseItemPlaced.DefenceItem = defenceItem;
            EventManager<OnDefenceItemPlaced>.CustomAction(this, onDefenseItemPlaced);
        }

        PoolType = EPoolObjectType.None;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(TryPlaceItem())
        {
            PlaceItem();
        }
    }

    private void OnButtonClicked(object sender, ButtonClickEvent clickEvent)
    {
        if(CurrentResource != null || !IsPlacablePiece)
            return;
        IsItemSelected = true;
        _clickedButtonType = clickEvent.PoolObjectType;
        PoolType = clickEvent.PoolObjectType;
        _visualController.TryChangePieceColor();
    }

    private bool TryPlaceItem()
    {
        if(CurrentResource != null || !IsPlacablePiece || PoolType == EPoolObjectType.None)
        {
            return false;
        }
        return true;
    }

    private void PlaceItem()
    {
        Debug.Log("Defence Item placed");
        IsItemSelected = false;
        var resource = ResourcePoolManager.Instance.LoadResource(_clickedButtonType, transform);
        CurrentResource = resource;
        SetPieceElement(resource);
    }

    private void OnDefenceItemPlace(object sender, OnDefenceItemPlaced @event)
    {
        PoolType = EPoolObjectType.None;
    }
}
