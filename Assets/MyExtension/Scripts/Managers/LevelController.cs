using UnityEngine;

public class LevelController : MonoBehaviour
{
    public LevelScriptable ActiveLevelScriptable { get => activeLevelScriptable; set => activeLevelScriptable = value; }
    private LevelScriptable activeLevelScriptable;
}
