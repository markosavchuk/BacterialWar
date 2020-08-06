using UnityEngine;

public class Settings : SingletonMonoBehaviour<Settings>
{
    protected override void OnAwake()
    {
        base.OnAwake();

        Application.targetFrameRate = 30;
    }

    [SerializeField]
    public float StepTime;
}
