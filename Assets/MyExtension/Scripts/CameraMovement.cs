using UnityEngine;
using DG.Tweening;
using System;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 gameEndOfsset;

    [SerializeField] private float lerpTime;

    private GameManager gameManager;

    private Transform activeBall;

    private Vector3 defaultAngle;

    private bool gameEnd = false;


    private void Awake()
    {
        defaultAngle = transform.eulerAngles;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;

        ActionManager.SetCamTarget += SetCamTarget;
        ActionManager.ActiveBall += SetActiveBall;
        ActionManager.GetActiveBall += GetActiveBall;

        gameManager.GameWin += GameWin;
        gameManager.GameFail += GameFail;
    }

    private void GetActiveBall()
    {
        ActionManager.ActiveBall?.Invoke(target.GetComponent<Ball>());
    }

    private void GameWin()
    {
        gameEnd = true;
        transform.eulerAngles = new Vector3(40, 0, 0);
    }

    private void GameFail()
    {
        target = null;
    }

    private void SetActiveBall(Ball ball)
    {
        DOTween.Complete(GetHashCode());

        if (ball == null)
        {
            transform.DORotate(new Vector3(defaultAngle.x,0, 0), .35f)
                .SetId(GetHashCode())
                .SetEase(Ease.OutSine);
            activeBall = null;
            return;
        }

        activeBall = ball.transform;
        target = activeBall.transform;
    }

    private void SetCamTarget(Transform target)
    {
        if (!gameManager.ExecuteGame && target.GetComponent<Ball>()) return;

        if (target != this.target)
            this.target = target;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        if(!gameEnd)
            GameActiveFollow();
        else
            GameWinFollow();
    }

    private void GameWinFollow()
    {
        Vector3 followPos = target.position + gameEndOfsset;

        transform.position = Vector3.Lerp(transform.position, followPos, lerpTime);
    }

    private void GameActiveFollow()
    {
        Vector3 followPos = activeBall ? target.position + target.forward * offset.z + target.up * offset.y : target.position + offset;

        transform.position = Vector3.Lerp(transform.position, followPos, lerpTime);

        var lookPos = (activeBall ? target.position + target.forward * -offset.z + target.up * -offset.y : target.position) - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookPos), lerpTime / 1.25f);
    }
}