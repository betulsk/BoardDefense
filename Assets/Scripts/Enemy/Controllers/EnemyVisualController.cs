using UnityEngine;

public class EnemyVisualController : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private void Start()
    {
        _enemy.OHealthUpdated += OnHealthUpdated;
    }

    private void OnDestroy()
    {
        _enemy.OHealthUpdated -= OnHealthUpdated;
    }

    private void OnHealthUpdated(int health)
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {

    }
}
