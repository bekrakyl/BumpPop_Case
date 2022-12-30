using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance { get => instance; set => instance = value; }
    public bool ExecuteGame { get => executeGame; set => executeGame = value; }

    public Action GameStart { get; set; }
    public Action GameWin { get; set; }
    public Action GameFail { get; set; }

    private bool executeGame = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        GameStart += Init;
        GameWin += GameEndWin;
        GameFail += GameEndFail;
    }

    private void Init()
    {
        executeGame = true;
    }
    private void GameEnd()
    {
        executeGame = false;
        GameStart -= Init;
        GameWin -= GameEndWin;
        GameFail -= GameEndFail;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RestartLevel();

        if (Input.GetKeyDown(KeyCode.N))
            NextLevel();

        if (Input.GetKeyDown(KeyCode.B))
            PreviousLevel();
    }
#endif

    private void GameEndWin()
    {
        Debug.Log("GameWin");
        GameEnd();
        //RunExtension.After(2f, () => NextLevel());
    }

    private void GameEndFail()
    {
        Debug.Log("GameFail");
        GameEnd();
        //RunExtension.After(2f, () => RestartLevel());
    }


    private void PreviousLevel()
    {
        PrefManager.ChangeLevel(-1);
        RestartLevel();
    }

    public void NextLevel()
    {
        PrefManager.ChangeLevel(1);
        RestartLevel();
    }

    public void RestartLevel()
    {
        ActionManager.ClearActionManagerData();
        SceneManager.LoadScene(StringUtil.SCENE_NAME);
    }

}
