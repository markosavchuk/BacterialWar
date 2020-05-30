using UnityEngine;

public class BaseFactoryMobProduction : MonoBehaviour
{
    private FactoryObject _factoryObject;

    [SerializeField]
    public GameObject ReproducableMob;

    [SerializeField]
    private float _reproducablePeriod = 3;

    [SerializeField]
    private Vector3 _mobOffset = new Vector3(0, 0.2f, 0);

    private float _time = 0f;
    private float _reproducableTime;

    private void Awake()
    {
        _factoryObject = GetComponent<FactoryObject>();
        _reproducableTime = _reproducablePeriod * Settings.Instance.StepTime;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _reproducableTime)
        {
            _time -= _reproducableTime;

            MakeNewMob();
        }
    }

    private void MakeNewMob()
    {
        if (_factoryObject.MobAbove != null || ReproducableMob==null)
        {
            return;
        }

        var newMobInstance = Instantiate(ReproducableMob);
        newMobInstance.transform.position = _factoryObject.transform.position + _mobOffset;
        newMobInstance.transform.parent = _factoryObject.transform;

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
