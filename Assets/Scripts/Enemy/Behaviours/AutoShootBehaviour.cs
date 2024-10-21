using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AutoShootBehaviour : MonoBehaviour
{
    private CancellationTokenSource _cancellationTokenSource;
    private readonly int _milliseconds = 1000;

    [SerializeField] private Enemy _enemy;

    private void OnEnable()
    {
        _cancellationTokenSource = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        _cancellationTokenSource.Cancel();
    }

    public async Task TryShoot(IHealthProvider healthProvider)
    {
        await Task.Delay(_enemy.Interval * _milliseconds);

        if(_enemy.IsDead || _cancellationTokenSource.IsCancellationRequested)
        {
            return;
        }
        StartAutoShoot(healthProvider);
        await _enemy.EnemyMovementBehaviour.TryMove();
    }

    public void StartAutoShoot(IHealthProvider healthProvider)
    {
        healthProvider.TakeDamage(_enemy.Damage);
    }
}
