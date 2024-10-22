public struct ButtonClickEvent 
{
    public EPoolObjectType PoolObjectType;
}

public struct OnDefenceItemPlaced
{
    public DefenseItem DefenceItem;
}

public struct OnLevelCompleted
{
    public bool IsWin;
}

public struct OnEnemyDied
{
    public Enemy Enemy;
}
