using UnityEngine;

public static class PlayerData
{
    public static int LevelData
    {
        get => PlayerPrefs.GetInt($"Level", 0);
        set => PlayerPrefs.SetInt($"Level", value);
    }
}
