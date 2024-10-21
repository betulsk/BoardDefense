using UnityEngine;

public class ItemUIWidget : MonoBehaviour
{
    [SerializeField] private ItemButtonWidget _buttonPrefab;

    private void Awake()
    {
        GameManager.Instance.OnBoardCreated += OnBoardCreated;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnBoardCreated -= OnBoardCreated;
        }
    }

    private void OnBoardCreated()
    {
        foreach(var itemData in GameConfigManager.Instance.GetDefenseItemData())
        {
            ItemButtonWidget buttonWidget =  Instantiate(_buttonPrefab, parent: transform);
            buttonWidget.SetData(itemData.DefenseItemPrefab.DefenseType, itemData.ItemCount);
        }
    }
}
