using UnityEngine;

public class BaseFactoryMobProduction : MonoBehaviour
{
    private FactoryObject _factoryObject;

    [SerializeField]
    public GameObject ReproducableMob;

    [SerializeField]
    private float _reproducablePeriod = 3;

    private float _time = 0f;
    private float _reproducableTime;

    protected GameObject NewMobInstance;

    private void Awake()
    {
        _factoryObject = GetComponent<FactoryObject>();
        _reproducableTime = _reproducablePeriod * Settings.Instance.StepTime;
    }

    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _reproducableTime)
        {
            _time -= _reproducableTime;

            MakeNewMob();
        }
    }

    protected virtual void MakeNewMob()
    {
        if (_factoryObject.MobAbove != null || ReproducableMob==null)
        {
            return;
        }

        NewMobInstance = Instantiate(ReproducableMob);
        NewMobInstance.transform.position = _factoryObject.transform.position;
        NewMobInstance.transform.parent = _factoryObject.transform;

        //todo move this to specific factories later
        var mobObject = NewMobInstance.AddComponent<MobObject>();
        mobObject.Player = _factoryObject.Player;        

        _factoryObject.SetMobAbove(mobObject);

        NewMobInstance.AddComponent<MobMovement>();
    }
}
