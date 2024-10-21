using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyMovementBehaviour : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    //public void StartMovement()
    //{
    //    _moveRoutine = StartCoroutine(TryMove());
    //}

    public async Task TryMove()
    {
        _enemy.CurrentBoardPiece.Neighbours.TryGetValue(EPieceDirectionType.Down, out var downPiece);

        if(downPiece == null)
        {
            return;
        }

        if(downPiece != null && downPiece.CurrentResource == null)
        {
            Move(downPiece);
        }
        else if(downPiece != null /*&& GameElementHelper.IsInCategory(downPiece.PieceType, Constants.CatogoryDefenseItem)*/)
        {
            var damagableElement = downPiece.CurrentResource as DefenseItem;
            if(damagableElement != null && !_enemy.IsDead)
            {
                await _enemy.AutoShootBehaviour.TryShoot(damagableElement);
            }
        }
        else
        {
            await Task.Delay(1500);
            await TryMove();
        }
    }

    private void Move(BoardPiece piece)
    {
        //_enemy.CurrentBoardPiece.PieceType = EPieceType.Empty;
        _enemy.CurrentBoardPiece.CurrentResource = null;
        _enemy.transform.DOLocalMoveY(0, _enemy.Speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(OnReachedToTargetTile);
    }

    private void OnReachedToTargetTile()
    {
        Debug.Log("OnReachedTarget");
    }
}
