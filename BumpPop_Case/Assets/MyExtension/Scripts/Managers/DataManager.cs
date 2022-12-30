using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get => instance; set => instance = value; }
    public GameData GameData { get => gameData; set => gameData = value; }

    private static DataManager instance;


    [SerializeField] private GameData gameData;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        GameData = GameData.ReadToJson();

        ActionManager.GameDataControl = () => GameData;
        ActionManager.UpdateData += UpdateData;

    }

    private void Start()
    {
        ActionManager.OnDataReaded?.Invoke(GameData);
    }

    private void UpdateData(GameData data)
    {
        data.WriteToJson();
    }
}

public static class Json
{
    private static string dataName = "GameData";

    public static void WriteToJson<T>(this T data)
    {
        string path = GetFilePath(dataName);
        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(path, json);
    }


    public static T ReadToJson<T>(this T data)
    {
        string path = GetFilePath(dataName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<T>(json);
            return data;
        }
        else
        {
            Debug.LogWarning("Data Couldnt Found");
            return data;
        }

    }
    private static string GetFilePath(string fileName)
    {
        return Application.dataPath + "/" + fileName + ".json";
    }
}