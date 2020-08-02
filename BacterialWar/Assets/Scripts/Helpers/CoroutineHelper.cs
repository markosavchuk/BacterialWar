using System;
using System.Collections;
using UnityEngine;

public static class CoroutineHelper
{
    public static IEnumerator ExecuteAfterTime(float waitingSeconds, Action executionTask,
        Action pingAction, float pingSeconds)
    {
        int fraction = (int)(waitingSeconds / pingSeconds);

        for (int i=0; i<fraction; i++)
        {
            yield return new WaitForSeconds(pingSeconds);
            pingAction();
        }

        yield return new WaitForSeconds(waitingSeconds - pingSeconds * fraction);
        executionTask();
    }

    public static IEnumerator ExecuteAfterTime(float waitingSeconds, Action executionTask)
    {
        yield return new WaitForSeconds(waitingSeconds);
        executionTask();
    }
}
