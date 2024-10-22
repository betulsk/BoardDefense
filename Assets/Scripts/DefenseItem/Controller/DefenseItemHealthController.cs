using UnityEngine;

public class DefenseItemHealthController : MonoBehaviour
{
    [SerializeField] private DefenseItem _defenseItem;

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

    }
}
