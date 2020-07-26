using UnityEngine;

public class Settings : SingletonMonoBehaviour<Settings>
{
    protected override void OnAwake()
    {
        base.OnAwake();

        //todo change it later
        Application.targetFrameRate = 30;
    }

    [SerializeField]
    public float StepTime;
}
