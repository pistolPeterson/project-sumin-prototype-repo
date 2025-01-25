using System;
using System.Collections;
using UnityEngine;

public class PeteUtility : MonoBehaviour
{
    public static IEnumerator WaitThenCall(Action method, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        method();
    }

}
