using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
using DG.Tweening;

public class BallThrower : MonoBehaviour
{
    [SerializeField] private BallControlSettings controlSettings;
    [SerializeField] private List<Transform> balls = new List<Transform>();

    [SerializeField] private Ball activeBall;

    private GameManager gameManager;
    protected InputManager inputManager;

    private LineRenderer line;

    private Coroutine findBallCoroutine;

    private Ray ray;
    private RaycastHit hit;

    private bool newBallSetted;

    private bool gameActive = false;

    private void Start()
    {
        line = GetComponentInParent<LineRenderer>();

        inputManager = GetComponent<InputManager>();
        gameManager = GameManager.Instance;

        GetFirstActiveBall();

        gameManager.GameStart += GameStart;
        ActionManager.ActiveBall += OnBallChoised;
    }

    private void GameStart()
    {
        RunExtension.After(.25f, () => gameActive = true);
    }

    private void GetFirstActiveBall()
    {
        List<Ball> balls = new List<Ball>();
        balls = transform.GetComponentsInChildren<Ball>().ToList();
        balls.Sort(bl => bl.transform.position.z);
        ActionManager.ActiveBall?.Invoke(balls.First());
    }

    private void Update()
    {
        if (!gameActive) return;

        if (Input.GetMouseButtonUp(0))
            ThrowBall();
    }

    private void FixedUpdate()
    {
        if ((!gameActive || activeBall == null) || !gameManager.ExecuteGame) return;

        CalculateDirection();

        ResetLine();

        CalculateLine();
    }

    private void CalculateDirection()
    {
        Vector3 dir = inputManager.Diff.normalized;
        dir.y += .5f;

        ray = new Ray(activeBall.transform.position, inputManager.Diff.magnitude > 0.02f ? new Vector3(-dir.x, 0, -dir.y) : new Vector3(dir.x, 0, dir.y));
    }

    private void CalculateLine()
    {
        float remainingLength = controlSettings.maxLength;

        for (int i = 0; i < controlSettings.reflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength, controlSettings.layerMask))
            {
                line.positionCount += 1;
                line.SetPosition(line.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
            }
            else
            {
                line.positionCount += 1;
                line.SetPosition(line.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }

        Vector3 lastLinePos = line.GetPosition(1);
        activeBall.transform.LookAt(new Vector3(lastLinePos.x, activeBall.transform.position.y, lastLinePos.z));
    }

    private void ThrowBall()
    {
        if (activeBall == null) return;

        Vector3 dir = (line.GetPosition(1) - activeBall.transform.position).normalized;
        float force = controlSettings.force;
        activeBall.ForceTheBall(dir, force);

        ResetLine();

        ActionManager.ActiveBall?.Invoke(null);

        newBallSetted = false;
    }

    private void ResetLine()
    {
        if (activeBall == null) return;

        line.positionCount = 1;
        line.SetPosition(0, activeBall.transform.position);
    }

    public void AddBall(Ball divisible)
    {
        balls.Add(divisible.transform);
    }

    public void CheckNewBall()
    {
        if (findBallCoroutine != null)
            StopCoroutine(findBallCoroutine);

        findBallCoroutine = StartCoroutine(SetNewBall());
    }

    private IEnumerator SetNewBall()
    {
        foreach (var item in balls)
        {
            Rigidbody body = item.GetComponent<Rigidbody>();
            DOTween.To(() => body.angularDrag, x => body.angularDrag = x, 5, 4f);
        }

        for (int i = 0; i < 5; i++)
        {
            FindNewBall();
            yield return new WaitForSeconds(.3f);
        }

        Ball ball = balls.GetLast().gameObject.GetComponent<Ball>();

        newBallSetted = true;

        yield return new WaitForSeconds(3f);

        if (gameManager.ExecuteGame)
            ActionManager.GetActiveBall?.Invoke();
        balls.Clear();
    }

    private void FindNewBall()
    {
        List<Transform> tempList = new List<Transform>();
        tempList.AddRange(balls);
        tempList.Sort(tr => tr.position.z);
        Transform last = tempList.GetLast();
        ActionManager.SetCamTarget?.Invoke(last);
    }

    private void OnBallChoised(Ball ball)
    {
        activeBall = ball;

        RunExtension.After(7f, () =>
        {
            if (gameManager.ExecuteGame && !newBallSetted)
                gameManager.GameFail?.Invoke();
        });
    }
}
