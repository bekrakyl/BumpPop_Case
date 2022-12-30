using UnityEngine;

public enum LevelButtonType
{
    NextLevel,
    RestartLevel
}

public class LevelButton : MonoBehaviour
{
    [SerializeField] private LevelButtonType buttonType;
    public void OnButtonPress()
    {
        if (buttonType == LevelButtonType.NextLevel)
            GameManager.Instance.NextLevel();
        if(buttonType == LevelButtonType.RestartLevel)
            GameManager.Instance.RestartLevel();
    }
}
