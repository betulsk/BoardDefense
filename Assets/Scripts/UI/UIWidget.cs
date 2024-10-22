using UnityEngine;

public class UIWidget : MonoBehaviour
{
    [SerializeField] private LevelWinWidget _levelWinWidget;
    [SerializeField] private LevelFailWidget _levelFailWidget;

    private void Start()
    {
        EventManager<OnLevelCompleted>.SubscribeToEvent(OnLevelFinished);
    }

    private void OnDestroy()
    {
        EventManager<OnLevelCompleted>.UnsubscribeToEvent(OnLevelFinished);
    }

    private void OnLevelFinished(object sender, OnLevelCompleted levelData)
    {
        if(levelData.IsWin)
        {
            _levelWinWidget.gameObject.SetActive(true);
            return;
        }
        _levelFailWidget.gameObject.SetActive(true);
    }
}
