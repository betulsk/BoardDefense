using UnityEngine;

public static class PlayerData
{
    public static int LevelData
    {
        get => PlayerPrefs.GetInt($"Level", 0);
        set => PlayerPrefs.SetInt($"Level", value);
    }

    public static void SaveData(string dataStr, int data)
    {
        PlayerPrefs.SetInt(dataStr, data);
        PlayerPrefs.Save();
    }

    public static void GetData(string dataStr, int data)
    {

    }
}
