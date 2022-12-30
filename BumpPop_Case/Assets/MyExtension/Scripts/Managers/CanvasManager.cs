using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get => instance; set => instance = value; }

    private static CanvasManager instance;

    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private GameObject gameFailPanel;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI levelComplatedText;

    private GameManager gameManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        levelText.text = "Lv. " + PrefManager.GetLevel;

        gameWinPanel.SetActive(false);
        gameFailPanel.SetActive(false);

        gameManager.GameWin += GameWin;
        gameManager.GameFail += GameFail;
    }

    private void GameWin()
    {
        //int levelIndex = PrefManager.GetLevel;
        //levelComplatedText.text = levelIndex + "." + " Level Compleated!";
        //SetPanel(gameWinPanel);
    }

    private void GameFail()
    {
        SetPanel(gameFailPanel);
    }

    private void SetPanel(GameObject panel)
    {
        panel.SetActive(true);
        panel.transform.DOScale(Vector3.one, 1.5f)
            .SetEase(Ease.OutBack)
            .SetId(GetHashCode());
    }
}
