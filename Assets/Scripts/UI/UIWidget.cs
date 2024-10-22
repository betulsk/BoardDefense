using TMPro;
using UnityEngine;

public class UIWidget : MonoBehaviour
{
    [SerializeField] private LevelWinWidget _levelWinWidget;
    [SerializeField] private LevelFailWidget _levelFailWidget;
    [SerializeField] private TMP_Text _levelText;

    private void Awake()
    {
        GameManager.Instance.OnBoardCreated += OnBoardCreated;
    }

    private void Start()
    {
        PhaseManager.Instance.OnPhaseChanged += OnPhaseChanged; 
        //EventManager<OnLevelCompleted>.SubscribeToEvent(OnLevelFinished);
    }

    private void OnDestroy()
    {
        PhaseManager.Instance.OnPhaseChanged -= OnPhaseChanged;

        //EventManager<OnLevelCompleted>.UnsubscribeToEvent(OnLevelFinished);
        GameManager.Instance.OnBoardCreated -= OnBoardCreated;
    }

    private void OnPhaseChanged(EPhase phase)
    {
        if(phase is EPhase.EndGamePhase)
        {
            if(PhaseManager.Instance.IsWin)
            {
                _levelWinWidget.gameObject.SetActive(true);
                return;
            }
            _levelFailWidget.gameObject.SetActive(true);
        }
    }

    private void OnBoardCreated()
    {
        int levelData = GameManager.Instance.CurrentLevelIndex + 1;
        Debug.Log("UI LevelData: " + levelData + " CurrentLevelIndex: " + GameManager.Instance.CurrentLevelIndex);
        _levelText.text = "Level " + levelData;   
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
