using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PooledObject
{
    public PoolType poolType;
    public int startingCreateCount;
    public GameObject prefab;
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get => instance; set => instance = value; }
    private static PoolManager instance;

    [SerializeField] private List<GameObject> onPools;
    [SerializeField] private List<GameObject> actives;
    [SerializeField] private List<PooledObject> pooledObjects;


    public int ActivesCount { get => actives.Count; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (pooledObjects.Count > 0)
            StartingCreate();

        ActionManager.GetItemFromPool += GetItem;
        ActionManager.ReturnItemToPool += ReturnPool;
        GameManager.Instance.GameWin += GameEnd;
    }

    private void StartingCreate()
    {
        foreach (var item in pooledObjects)
        {
            for (int i = 0; i < item.startingCreateCount; i++)
            {
                GameObject cube = Instantiate(item.prefab, transform);
                cube.transform.position = Vector3.zero;
                onPools.Add(cube);
                cube.SetActive(false);
            }
        }
    }

    private GameObject GetItem(PoolType type, Vector3 position, Transform parent)
    {
        GameObject cube = null;
        PooledObject pooled = pooledObjects.Find(obj => obj.poolType == type);

        if (parent == null)
            Debug.Log("Parent null");

        if (onPools.Count == 0)
        {
            cube = Instantiate(pooled.prefab, parent);
            cube.transform.position = position;
            actives.Add(cube);
            return cube;
        }
        else
        {
            cube = onPools.Last();
            onPools.Remove(cube);
            actives.Add(cube);
            cube.transform.parent = parent;
            cube.transform.position = position;
            cube.SetActive(true);
            return cube;
        }
    }

    private void ReturnPool(GameObject arg1, PoolType arg2)
    {
        PooledObject pooled = pooledObjects.Find(obj => obj.poolType == arg2);
        actives.Remove(arg1);
        onPools.Add(arg1);
        arg1.SetActive(false);
        arg1.transform.parent = transform;
    }

    private void GameEnd()
    {
        //ActionManager.GetItemFromPool -= GetItem;
        //ActionManager.ReturnItemToPool -= ReturnPool;
        //GameManager.Instance.GameWin -= GameEnd;
    }
}
