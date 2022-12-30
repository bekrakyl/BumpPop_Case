using Sirenix.Utilities;
using TMPro;
using UnityEngine;

public class Chain : MonoBehaviour, ICollisionChain
{
    [SerializeField] private Transform middleChain;
    [SerializeField] private TextMeshPro chainHealthText;

    [SerializeField] private int chainHealth;

    private bool firstInteract = false;

    private void Start()
    {
        chainHealthText.text = chainHealth.ToString();
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
        chainHealthText.text = chainHealth.ToString();

        if (chainHealth <= 0)
            CrashChain();
    }

    private void CrashChain()
    {
        Destroy(middleChain.gameObject);
        Rigidbody[] rigidbodies = transform.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(Vector3.forward * 3f, ForceMode.Impulse);

            //BoxCollider boxCollider = rb.GetComponent<BoxCollider>();
            //Destroy(boxCollider);
        }
    }
}
