using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyMovementBehaviour : MonoBehaviour
{
    private Coroutine _moveRoutine;
    [SerializeField] private Enemy _enemy;

    public void StartMovement()
    {
        _moveRoutine = StartCoroutine(TryMove());
    }

    public IEnumerator TryMove()
    {
        _enemy.CurrentBoardPiece.Neighbours.TryGetValue(EPieceDirectionType.Down, out var downPiece);

        if(downPiece == null)
        {
            yield return null;
        }

        if(downPiece != null && downPiece.PieceType == EPieceType.Empty)
        {
            Move(downPiece);
        }
        else if(downPiece != null /*&& GameElementHelper.IsInCategory(downPiece.PieceType, Constants.CatogoryDefenseItem)*/)
        {
            //var damagableElement = downPiece.Resource as DefenceItem;
            //if(damagableElement != null && _canAttack)
            //{
            //    await TryAttack(damagableElement);
            //}
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(TryMove());
        }
    }

    private void Move(BoardPiece piece)
    {
        _enemy.CurrentBoardPiece.PieceType = EPieceType.None;
        _enemy.transform.DOLocalMoveY(0, _enemy.MovementSpeed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(OnReachedToTargetTile);

    }

    private void OnReachedToTargetTile()
    {
        Debug.Log("OnReachedTarget");
    }
}
