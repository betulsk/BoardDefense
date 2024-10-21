using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonWidget : MonoBehaviour
{
    private const string SPACE = " ";
    private int _counter;

    [SerializeField] private EDefenseItemType _defenseType;
    [SerializeField] private EPoolObjectType _poolType;
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

    public void SetData(DefenseData defenseData)
    {
        _counter = defenseData.ItemCount;
        _defenseType = defenseData.DefenseItemPrefab.DefenseType;
        _poolType = defenseData.PoolType;
        SetTotalCountText();
    }

    private void OnButtonClicked()
    {
        _counter--;
        SetTotalCountText();
        if (_counter <= 0)
        {
            DisableButton();
        }
        ButtonClickEvent buttonClickEvent = new ButtonClickEvent();
        buttonClickEvent.PoolObjectType = _poolType;
        EventManager<ButtonClickEvent>.CustomAction(this, buttonClickEvent);
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
