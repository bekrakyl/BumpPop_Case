using System;
using System.Collections;
using UnityEngine;

public class RunExtension
{
    public static RunExtension After(float aDelay, Action aAction)
    {
        var tmp = new RunExtension();
        Managers.Instance.StartCoroutine(_RunAfter(aDelay, aAction));
        return tmp;
    }
    private static IEnumerator _RunAfter(float aDelay, Action aAction)
    {
        yield return new WaitForSeconds(aDelay);
        aAction?.Invoke();
    }
}
