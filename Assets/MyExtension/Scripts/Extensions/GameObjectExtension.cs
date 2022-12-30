using System.Linq;
using UnityEngine;

public static class GameObjectExtension
{
    public static void CenterOnChildred(this Transform aParent)
    {
        var childs = aParent.Cast<Transform>().ToList();
        var pos = new Vector3(0, 0.5f, 0);
        foreach (var C in childs)
        {
            pos += C.position;
            C.parent = null;
        }
        pos /= childs.Count;
        aParent.position = pos;
        foreach (var C in childs)
            C.parent = aParent;
    }

    public static void SetLayer(this GameObject obj, string layer)
    {
        int layerIndex = LayerMask.NameToLayer(layer);
        obj.layer = layerIndex;

    }


    public static void RemoveComponent(this GameObject obj, Component component)
    {
        
    }

}
