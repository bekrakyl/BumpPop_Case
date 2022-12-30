using UnityEngine;

public class FinishController : MonoBehaviour, ICollisionFinish
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void OnInteractFinish(Rigidbody body)
    {
        if (gameManager.ExecuteGame)
            gameManager.GameWin?.Invoke();
    }
}
