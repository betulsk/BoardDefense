using System;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PhaseManager : Singleton<PhaseManager>
{
    public bool IsGamePhaseStarted;

    public bool IsGamePhaseFinished;

    public EPhase CurrentPhaseType;
    public Action<EPhase> OnPhaseChanged;

    private void Awake()
    {
        ChangePhase(EPhase.GamePhase);
        EventManager<OnLevelCompleted>.SubscribeToEvent(OnLevelComplete);
    }

    private void OnDestroy()
    {
        EventManager<OnLevelCompleted>.UnsubscribeToEvent(OnLevelComplete);
    }

    private void OnLevelComplete(object sender, OnLevelCompleted @event)
    {
        ChangePhase(EPhase.EndGamePhase);
    }

    public void ChangePhase(EPhase phase)
    {
        CurrentPhaseType = phase;
        OnPhaseChanged?.Invoke(CurrentPhaseType);
    }

    public bool CheckPhase()
    {
        if(CurrentPhaseType is EPhase.GamePhase)
        {
            IsGamePhaseStarted = true;
            return IsGamePhaseStarted;
        }
        if(CurrentPhaseType is EPhase.EndGamePhase)
        {
            IsGamePhaseFinished = true;
            return IsGamePhaseFinished;
        }
        return false;
    }
}

public enum EPhase
{
    None = 0,
    MainMenuPhase = 1,
    GamePhase = 2,
    EndGamePhase = 3,
}
