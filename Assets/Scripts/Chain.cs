using TMPro;
using UnityEngine;

public class Chain : MonoBehaviour, ICollisionChain
{
    [SerializeField] private Transform middleChain;
    [SerializeField] private TextMeshPro chainHealthText;

    [SerializeField] private int chainHealth;

    private bool firstInteract = false;

    private bool crashed;

    private void Start()
    {
        SetChainText();
    }

    public void OnCollisionChain()
    {
        if (chainHealth <= 0) return;

        if (!firstInteract)
        {
            ActionManager.SetCamTarget?.Invoke(transform);
            firstInteract = true;
        }
        chainHealth--;
        SetChainText();

        if (chainHealth <= 0)
            CrashChain();

        RunExtension.After(8.5f, () => CheckCrash());

    }

    private void CheckCrash()
    {
        if (crashed) return;

        ActionManager.OpenWinPanel?.Invoke();
    }

    private void CrashChain()
    {
        if (middleChain == null) return;

        Destroy(middleChain.gameObject);
        Rigidbody[] rigidbodies = transform.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(Vector3.forward * 10f, ForceMode.Impulse);
            BoxCollider boxCollider = rb.gameObject.GetComponent<BoxCollider>();
            Destroy(boxCollider);
            rb.gameObject.AddComponent<CapsuleCollider>();
        }
        crashed = true;
    }

    private void SetChainText()
    {
        if (chainHealthText == null) return;

        chainHealthText.text = chainHealth.ToString();
    }
}
