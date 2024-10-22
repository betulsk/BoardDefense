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
    }

    private void OnDestroy()
    {
        PhaseManager.Instance.OnPhaseChanged -= OnPhaseChanged;
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
        _levelText.text = "Level " + levelData;   
    }
}
