using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Settings/BallControlSettings", fileName = "BallControlSettings.asset")]
public class BallControlSettings : ScriptableObject
{
    public LayerMask layerMask;
    public float sensitivity;
    public int reflections;
    public float maxLength;
    public float force;
}
