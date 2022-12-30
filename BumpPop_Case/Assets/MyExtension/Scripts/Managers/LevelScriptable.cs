using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Level ", menuName = "Level/LevelScriptable")]
public class LevelScriptable : ScriptableObject
{
    [AssetList(Path = "Prefabs/LevelPrefabs")]
    public GameObject levelPrefab;
}
