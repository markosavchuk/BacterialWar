using UnityEngine;

public class Settings : SingletonMonoBehaviour<Settings>
{
    private void Awake()
    {
        //todo change it later
        Application.targetFrameRate = 30;
    }

    [SerializeField]
    public float StepTime;
}
