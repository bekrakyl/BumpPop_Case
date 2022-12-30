using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Managers/MaterialManager", fileName = "MaterialManager.asset")]
public class MaterialManager : ScriptableObject
{
    [SerializeField] private Material[] ballMaterials;

    public Material GetBallMaterial(int matIndex)
    {
        if (ballMaterials.Length == 0) return null;

        return matIndex >= 0 ? ballMaterials[matIndex] : ballMaterials.RandomAt();
    }
}
