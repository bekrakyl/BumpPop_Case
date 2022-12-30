using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Managers/PrefabManager", fileName = "PrefabManager.asset")]
public class PrefabManager : ScriptableObject
{
    [SerializeField] private GameObject divisiblePrefab;

    public GameObject GetDivisable(Transform parent, Vector3 position)
    {
        GameObject divisable = Instantiate(divisiblePrefab, parent);
        divisable.transform.position = position;
        return divisable;
    }
}
