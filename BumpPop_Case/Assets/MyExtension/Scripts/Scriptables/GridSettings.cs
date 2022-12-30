using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Settings/GridSettings", fileName = "GridSettings.asset")]
public class GridSettings : ScriptableObject
{
    public Vector2Int gridSize;
}
