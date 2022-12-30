using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelScriptable activeLevel;
    [SerializeField] private LevelController levelController;

    private void Awake()
    {
        //ClearOnSceneLevel();

        if (GetLenght() > 0)
            LoadLevel();
    }

   
    private int GetLenght()
    {
        int lenght = Resources.LoadAll("Levels").Length;
        return lenght;
    }

    private void LoadLevel()
    {
        int levelIndex = PrefManager.GetLevel;
        LevelScriptable levelScriptable = Resources.Load(StringUtil.LEVEL_SCRIPTABLE_PATH + levelIndex) as LevelScriptable;

        if (levelScriptable == null)
        {
            int allLevelsCount = Resources.LoadAll<LevelScriptable>("Levels").Length;
            levelIndex = ((PrefManager.GetLevel - 1) % allLevelsCount) + 1;

            levelScriptable = Resources.Load(StringUtil.LEVEL_SCRIPTABLE_PATH + levelIndex) as LevelScriptable;
        }

        LevelController level = Instantiate(levelScriptable.levelPrefab).GetComponent<LevelController>();
        level.ActiveLevelScriptable = levelScriptable;
        this.activeLevel = levelScriptable;
        this.levelController = level;
    }

    private static void ClearOnSceneLevel()
    {
        LevelController[] sceneLevel = FindObjectsOfType<LevelController>();

        for (int i = 0; i < sceneLevel.Length; i++)
        {
            Destroy(sceneLevel[i].gameObject);
        }
    }
}
