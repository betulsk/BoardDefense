using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyMovementBehaviour : MonoBehaviour
{
    private readonly int Delay = 1500;
    [SerializeField] private Enemy _enemy;

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
        else if(downPiece != null && CategoryHelper.IsInCategory(downPiece.CurrentResource.PoolObjectType, Consts.CatogoryDefenseItem))
        {
            var damagableElement = downPiece.CurrentResource as DefenseItem;
            if(damagableElement != null && !_enemy.IsDead)
            {
                await _enemy.AutoShootBehaviour.TryShoot(damagableElement);
            }
        }
        else
        {
            await Task.Delay(Delay);
            await TryMove();
        }
    }

    private void Move(BoardPiece piece)
    {
        _enemy.CurrentBoardPiece.CurrentResource = null;
        piece.SetPieceElement(_enemy);
        _enemy.transform.DOLocalMoveY(0, _enemy.Speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(OnReachedToTargetTile);
    }

    public async void OnReachedToTargetTile()
    {
        var isLevelFailed = _enemy.CurrentBoardPiece.GridPosition.y == 0;
        if(isLevelFailed)
        {
            OnLevelCompleted levelCompleteEvent = new OnLevelCompleted();
            levelCompleteEvent.IsWin = false;
            EventManager<OnLevelCompleted>.CustomAction(this, levelCompleteEvent);
        }
        else
        {
            await TryMove();
        }
    }
}
