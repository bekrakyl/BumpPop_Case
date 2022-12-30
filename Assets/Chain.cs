using Sirenix.Utilities;
using System;
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

        RunExtension.After(7f, () => CheckCrash());
    }

    private void CheckCrash()
    {
        if (crashed) return;

        ActionManager.OpenWinPanel?.Invoke();

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
        crashed = true;
    }
}
