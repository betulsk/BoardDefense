using UnityEngine;

public class PieceVisualController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoardPiece _piece;

    public void TryChangePieceColor()
    {
        if(_piece.CurrentResource != null && _piece.IsPlacablePiece)
        {
            _spriteRenderer.color = GameConfigManager.Instance.GetActiveConfigData().PassivePieceColor;
            return;
        }
        if(_piece.IsPlacablePiece && _piece.CurrentResource == null)
        {
            _spriteRenderer.color = GameConfigManager.Instance.GetActiveConfigData().ActivePieceColor;
        }
    }
}
