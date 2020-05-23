using UnityEngine;

public class FreezeFactoryBuilder : MonoBehaviour, IBuilder
{
    [SerializeField]
    private GameObject _factoryLevel1Prefab;

    [SerializeField]
    private GameObject _factoryLevel2Prefab;

    [SerializeField]
    private GameObject _factoryLevel3Prefab;

    public GameObject GetStartFactoryPrefab()
    {
        return _factoryLevel1Prefab;
    }
}
