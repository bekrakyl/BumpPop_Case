using UnityEngine;
public class Managers : MonoBehaviour
{
    private static Managers instance;
    public static Managers Instance { get => instance;}
    public PrefabManager PrefabManger { get => prefabManger; }
    public MaterialManager MaterialManager { get => materialManager;}

    #region Managers
    [SerializeField] private PrefabManager prefabManger;
    [SerializeField] private MaterialManager materialManager;
    #endregion


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //gameObject.hideFlags = HideFlags.HideInHierarchy;
    }
}