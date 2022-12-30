using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSettings.asset", menuName = "Scriptables/Settings/PlayerMovementSettings")]
public class PlayerMovementSettings : ScriptableObject
{
    public float movementSpeed;
    public float sensivity;
}
