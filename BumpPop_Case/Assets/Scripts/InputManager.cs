using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private BallControlSettings controlSettings;
    private GameManager gameManager;

    private Camera ortho;

    private Vector3 diff;
    private Vector3 firstPos;
    private Vector3 mousePos;

    public Vector3 Diff { get => diff; set => diff = value; }

    private void Start()
    {
        gameManager = GameManager.Instance;

        ortho = ObjectManager.Instance.orthoCamera;
    }

    void Update()
    {
        if (!gameManager.ExecuteGame) return;

        if (Input.GetMouseButtonDown(0))
            MouseDown(Input.mousePosition);

        else if (Input.GetMouseButton(0))
            MouseHold(Input.mousePosition);

        else if (Input.GetMouseButtonUp(0))
            MouseUp();
    }

    private void MouseDown(Vector3 inputPos)
    {
        mousePos = ortho.ScreenToWorldPoint(inputPos);
        firstPos = mousePos;
    }

    private void MouseHold(Vector3 inputPos)
    {
        mousePos = ortho.ScreenToWorldPoint(inputPos);
        diff = mousePos - firstPos;
        diff *= controlSettings.sensitivity;
    }

    private void MouseUp()
    {
        diff = Vector3.zero;
    }
}
