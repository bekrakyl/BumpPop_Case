using Sirenix.Utilities;
using System;
using System.Reflection;
using UnityEngine;

public static class ActionManager
{
    public static Func<PoolType, Vector3, Transform, GameObject> GetItemFromPool { get; set; }
    public static Action<GameObject, PoolType> ReturnItemToPool { get; set; }
    public static Func<GameData> GameDataControl { get; set; }
    public static Action<GameData> OnDataReaded { get; set; }
    public static Action<GameData> UpdateData { get; set; }
    public static Action<Ball> ActiveBall { get; set; }
    public static Action<Transform> SetCamTarget { get; set; }

    public static void ClearActionManagerData()
    {
        var info = typeof(ActionManager)
        .GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        info.ForEach(a => a.SetValue(a.Name, null));
    }
}
