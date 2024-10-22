using UnityEngine;

public class PieceVisualController : MonoBehaviour
{
    private Color _defaultColor;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoardPiece _piece;

    private void Awake()
    {
        _defaultColor = _spriteRenderer.color;
        EventManager<OnDefenceItemPlaced>.SubscribeToEvent(SetDefaultColor);
    }

    private void OnDestroy()
    {
        EventManager<OnDefenceItemPlaced>.UnsubscribeToEvent(SetDefaultColor);
    }

    public void TryChangePieceColor()
    {
        if(_piece.CurrentResource != null)
        {
            if(_piece.IsPlacablePiece)
            {
                _spriteRenderer.color = GameConfigManager.Instance.GetActiveConfigData().PassivePieceColor;
            }
            return;
        }
        if(_piece.IsPlacablePiece && _piece.CurrentResource == null)
        {
            _spriteRenderer.color = GameConfigManager.Instance.GetActiveConfigData().ActivePieceColor;
        }
    }

    private void SetDefaultColor(object sender, OnDefenceItemPlaced @event)
    {
        _spriteRenderer.color = _defaultColor;
    }
}
