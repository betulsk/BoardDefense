using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class DefenseItemShootBehaviour : MonoBehaviour
{
    private readonly int _milliSeconds = 1000;
    private CancellationTokenSource _cancellationTokenSource;

    [SerializeField] private DefenseItem _defenseItem;

    private void OnEnable()
    {
        _cancellationTokenSource = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        _cancellationTokenSource.Cancel();
    }

    public async void TryAttack()
    {
        while(_defenseItem.CanAttack)
        {
            if(_defenseItem.IsDead || _cancellationTokenSource.IsCancellationRequested)
                return;

            var currentState = PhaseManager.Instance.CurrentPhaseType;
            if(currentState is not EPhase.GamePhase)
            {
                _defenseItem.CanAttack = false;
                return;
            }

            await Task.Delay((int)_defenseItem.Interval * _milliSeconds);
            if(_defenseItem.IsDead)
            {
                return;
            }

            Attack();
        }
    }

    private void Attack()
    {
        _defenseItem.AttackablePieces.ForEach(tile =>
        {
            if(tile.CurrentResource is Enemy enemy)
            {
                enemy.TakeDamage(_defenseItem.Damage);
            }
        });
    }
}
