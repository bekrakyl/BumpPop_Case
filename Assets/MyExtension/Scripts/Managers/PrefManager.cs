using UnityEngine;

public static class PrefManager
{
    public static int GetLevel => PlayerPrefs.GetInt(StringUtil.PREF_LEVEL, 1);

    public static void SetLevel(int value) => PlayerPrefs.SetInt(StringUtil.PREF_LEVEL, value); 


    public static void ChangeLevel(int value)
    {
        int level = GetLevel;
        SetLevel(level + value);
    }
}
