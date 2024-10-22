using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyVisualController : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] public Slider _slider;

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
        Debug.Log("Slider value is : " + fillAmount);
        DOTween.To(() => _slider.value, x => _slider.value = x, fillAmount, .4f);

    }
}
