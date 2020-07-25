using UnityEngine;

public class BaseFactoryMobProduction : MonoBehaviour
{    
    public GameObject ReproducableMob;

    [SerializeField]
    private Vector3 _mobOffset = new Vector3(0, 0.2f, 0);

    protected ProgressBarController ProgressBarControl;
    protected FactoryObject FactoryObject;
    protected MobObject MobObject;

    private float _time = 0f;
    private float _reproducableTime;

    protected virtual void Start()
    {
        FactoryObject = GetComponent<FactoryObject>();
        CalculateReproducableTime();

        AddProgressBar();        
    }

    private void Update()
    {
        if (!CanMakeNewMob())
        {
            return;
        }

        _time += Time.deltaTime;

        ProgressBarControl.TrackProgress(this, _time / _reproducableTime);

        if (_time >= _reproducableTime)
        {
            _time -= _reproducableTime;

            MakeNewMob();
        }
    }

    public virtual void OnUpgrade()
    {
        CalculateReproducableTime();
        InitializeParameters();
    }

    protected virtual void AddProgressBar()
    {
        var progressBarObject = Instantiate(UICollection.Instance.ProgressBar, UICollection.Instance.ProgressBarsCanvas.transform);

        ProgressBarControl = progressBarObject.GetComponent<ProgressBarController>();
        ProgressBarControl.SetPosition(FactoryObject.ParentHex.gameObject);
    }

    private bool CanMakeNewMob()
    {
        if (FactoryObject.MobAbove != null ||
            ReproducableMob == null ||
            StateManager.Instance.GameState == GameState.Lost)
        {
            ProgressBarControl.gameObject.SetActive(false);

            _time = 0;

            return false;
        }

        ProgressBarControl.gameObject.SetActive(true);
        return true;
    }

    private void MakeNewMob()
    {
        if (!CanMakeNewMob())
        {
            return;
        }

        var newMobInstance = Instantiate(ReproducableMob);
        newMobInstance.transform.position = FactoryObject.transform.position + _mobOffset;
        newMobInstance.transform.parent = MobCollection.Instance.gameObject.transform;

        MobObject = newMobInstance.AddComponent<MobObject>();
        MobObject.Player = FactoryObject.Player;

        FactoryObject.SetMobAbove(MobObject);

        newMobInstance.AddComponent<MobMovement>();

        InitializeNewMob(newMobInstance);
    }

    protected virtual void InitializeNewMob(GameObject mobInstance)
    {
        InitializeParameters();
    }

    private void CalculateReproducableTime()
    {
        _reproducableTime = FactoryParameters.GenerationSpeedConst / FactoryObject.Parameters.GenetaionSpeed
            * Settings.Instance.StepTime;
    }

    private void InitializeParameters()
    {
        MobObject.Health = FactoryObject.Parameters.Health;
        MobObject.StartHealth = FactoryObject.Parameters.Health;
        MobObject.RiachRange = FactoryObject.Parameters.RiachRange;
    }

    private void OnDestroy()
    {
        Destroy(ProgressBarControl.gameObject);
    }
}
