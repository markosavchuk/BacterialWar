using System;
using System.Collections;
using UnityEngine;

public static class CoroutineHelper
{
    public static IEnumerator ExecuteAfterTime(float waitingSeconds, Action executionTask)
    {
        yield return new WaitForSeconds(waitingSeconds);
        executionTask();
    }
}
