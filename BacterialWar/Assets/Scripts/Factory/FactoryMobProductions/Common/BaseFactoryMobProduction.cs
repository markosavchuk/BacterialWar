using UnityEngine;

public class BaseFactoryMobProduction : MonoBehaviour
{
    private FactoryObject _factoryObject;

    protected ProgressBarController ProgressBarControl;

    [SerializeField]
    public GameObject ReproducableMob;

    [SerializeField]
    public float ReproducablePeriod = 1.5f;

    [SerializeField]
    private Vector3 _mobOffset = new Vector3(0, 0.2f, 0);

    private float _time = 0f;
    private float _reproducableTime;

    private void Awake()
    {
        _factoryObject = GetComponent<FactoryObject>();
        _reproducableTime = ReproducablePeriod * Settings.Instance.StepTime;

        AddProgressBar();
    }

    protected virtual void AddProgressBar()
    {
        var progressBarObject = Instantiate(UICollection.Instance.ProgressBar, UICollection.Instance.ProgressBarsCanvas.transform);

        ProgressBarControl = progressBarObject.GetComponent<ProgressBarController>();
        ProgressBarControl.SetPosition(_factoryObject.ParentHex.gameObject);
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

    private bool CanMakeNewMob()
    {
        if (_factoryObject.MobAbove != null || ReproducableMob == null)
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
        newMobInstance.transform.position = _factoryObject.transform.position + _mobOffset;
        newMobInstance.transform.parent = MobCollection.Instance.gameObject.transform;

        var mobObject = newMobInstance.AddComponent<MobObject>();
        mobObject.Player = _factoryObject.Player;

        _factoryObject.SetMobAbove(mobObject);

        newMobInstance.AddComponent<MobMovement>();

        InitializeNewMob(newMobInstance);
    }

    protected virtual void InitializeNewMob(GameObject mobInstance)
    {

    }
}
