using System.Collections.Generic;
using UnityEngine;

public class ItemUIWidget : MonoBehaviour
{
    private List<ItemButtonWidget> _buttons;
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
        EventManager<OnDefenceItemPlaced>.UnsubscribeToEvent(OnItemPlaced);

    }

    public void EnableAllButtons()
    {
        foreach(var button in _buttons)
        {
            button.EnableButton();
        }
    }

    public void DisableAllButtons()
    {
        foreach(var button in _buttons)
        {
            button.DisableButton();
        }
    }

    private void OnBoardCreated()
    {
        _buttons = new List<ItemButtonWidget>();
        foreach(var itemData in GameConfigManager.Instance.GetDefenseItemDataList())
        {
            ItemButtonWidget buttonWidget = Instantiate(_buttonPrefab, parent: transform);
            buttonWidget.SetData(itemData);
            buttonWidget.ItemUIWidget = this;
            EventManager<OnDefenceItemPlaced>.SubscribeToEvent(OnItemPlaced);
            _buttons.Add(buttonWidget);
        }
    }

    private void OnItemPlaced(object sender, OnDefenceItemPlaced @event)
    {
        //EnableAllButtons();
    }
}
