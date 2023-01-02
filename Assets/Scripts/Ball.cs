using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour, ICollisionBall
{
    public bool Divised { get => divised; set => divised = value; }

    public Vector3 Dir { get => dir; set => dir = value; }

    private GameManager gameManager;
    private Managers managers;



    private MaterialManager materialManager;
    private PrefabManager prefabManager;
    private BallThrower ballThrower;

    private MeshRenderer mRenderer;
    private Rigidbody body;

    private Material ballMat;

    private Vector3 dir;

    private bool divised;
    private bool throwed = false;

    [SerializeField] private GameObject targetSprite;

    private void Start()
    {
        managers = Managers.Instance;
        prefabManager = managers.PrefabManger;
        materialManager = managers.MaterialManager;
        gameManager = GameManager.Instance;

        body = GetComponent<Rigidbody>();
        ballThrower = GetComponentInParent<BallThrower>();
        mRenderer = GetComponent<MeshRenderer>();

        SetBallMaterial();

        ActionManager.ActiveBall += OnBallChoised;
    }

    private void OnBallChoised(Ball ball)
    {
        if (ball == null) 
        {
            if (targetSprite.activeSelf)
                targetSprite.SetActive(false);
            return; 
        }

        if (!throwed && !divised)
        {
            targetSprite.SetActive(true);
            targetSprite.transform.LookAt(Camera.main.transform.position);
        }
    }

    private void SetBallMaterial()
    {
        ballMat = materialManager.GetBallMaterial(-1);
        mRenderer.material = ballMat;
    }

    public void ForceTheBall(Vector3 dir, float force)
    {
        divised = true;
        body.AddForce(dir * force);
        body.angularVelocity = dir * 3;
        this.dir = (dir * force) / 2;
        throwed = true;

        DOTween.To(() => body.angularDrag, x => body.angularDrag = x, 5, 4f);
    }

    


    public Ball OnInteractEnter(Ball ball)
    {
        if (this.throwed) return null;
        if (divised) return this;

        divised = true;

        ballThrower.AddBall(this);

        body.AddForce(ball.Dir);
        body.angularVelocity = ball.Dir;
        body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z);
        //thisBody.angularDrag = 3;

        SetLayer(gameObject);

        for (int i = 0; i < 6; i++)
        {
            GameObject newBall = prefabManager.GetDivisable(transform.parent, transform.position + Vector3.forward * .5f + Vector3.right * Random.Range(-.5f, .5f));
            ScaleUpFromZero(newBall);

            Ball divisedBall = newBall.GetComponent<Ball>();
            divisedBall.GetComponent<MeshRenderer>().material = ballMat;

            SetLayer(newBall);

            divisedBall.Divised = true;

            Rigidbody newBody = newBall.gameObject.GetComponent<Rigidbody>();
            newBody.AddForce(Vector3.forward * 5f, ForceMode.Impulse);
            newBody.velocity = body.velocity + Vector3.right * Random.Range(-10f, 10f);
            newBody.velocity = new Vector3(newBody.velocity.x, 0, newBody.velocity.z);

            ballThrower.AddBall(divisedBall);

        }

        RunExtension.After(.15f, () => ballThrower.CheckNewBall());

        return this;
    }

    private void ScaleUpFromZero(GameObject newBall)
    {
        newBall.transform.DOScale(Vector3.zero, .75f).From()
            .SetEase(Ease.OutBounce)
            .SetId(GetHashCode());
    }

    private static void SetLayer(GameObject obj)
    {
        obj.SetLayer(StringUtil.LAYER_IGNORECOLLISION);
        RunExtension.After(.5f, () => obj.SetLayer(StringUtil.LAYER_DEFAULT));
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<ICollisionFinish>()?.OnInteractFinish(body);
        other.gameObject.GetComponent<ICollisionChain>()?.OnCollisionChain();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<ICollisionBall>()?.OnInteractEnter(this);
    }
}
