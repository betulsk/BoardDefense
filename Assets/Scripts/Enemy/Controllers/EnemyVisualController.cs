using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyVisualController : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] public Slider _slider;
    [SerializeField] public float _duration = 0.4f;

    private void Awake()
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
        float fillAmount = (float)_enemy.GetCurHealth() / _enemy.GetMaxHealth();
        DOTween.To(() => _slider.value, x => _slider.value = x, fillAmount, _duration);
    }
}
