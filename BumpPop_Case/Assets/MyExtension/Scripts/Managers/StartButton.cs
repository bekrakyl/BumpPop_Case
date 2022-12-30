using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void OnButtonPressed()
    {
        GameManager.Instance.GameStart?.Invoke();
        gameObject.SetActive(false);
    }
}
