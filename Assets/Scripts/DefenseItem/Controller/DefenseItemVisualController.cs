using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DefenseItemVisualController : MonoBehaviour
{
    [SerializeField] private DefenseItem _defenseItem;
    [SerializeField] public Slider _slider;

    private void Start()
    {
        _defenseItem.OHealthUpdated += OnHealthUpdated;    
    }

    private void OnDestroy()
    {
        _defenseItem.OHealthUpdated -= OnHealthUpdated;
    }

    private void OnHealthUpdated(int health)
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        float fillAmount = (float)_defenseItem.GetCurHealth() / _defenseItem.GetMaxHealth();
        DOTween.To(() => _slider.value, x => _slider.value = x, fillAmount, .4f);
    }
}
