using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonWidget : MonoBehaviour
{
    private const string SPACE = " ";
    private int _counter;

    [SerializeField] private EDefenseItemType _defenseType;
    [SerializeField] private Button _button;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Color _disableColor;
    [SerializeField] private TMP_Text _itemCount;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void SetData(EDefenseItemType defenseType, int count)
    {
        _counter = count;
        _defenseType = defenseType;
        SetTotalCountText();
    }

    private void OnButtonClicked()
    {
        _counter--;
        SetTotalCountText();
        if (_counter <= 0)
        {
            DisableButton();
            return;
        }
    }

    private void DisableButton()
    {
        _button.enabled = false;
        _buttonImage.color = _disableColor;
    }

    private void SetTotalCountText()
    {
        _itemCount.text = _counter + SPACE + _defenseType;
    }
}
