using UnityEngine;

public class FreezeFactoryBuilder : MonoBehaviour, IBuilder
{
    [SerializeField]
    private GameObject _factoryLevel1Prefab;

    [SerializeField]
    private GameObject _factoryLevel2Prefab;

    [SerializeField]
    private GameObject _factoryLevel3Prefab;

    public GameObject GetFactoryPrefab(FactoryObject factory = null)
    {
        if (factory == null)
        {
            return _factoryLevel1Prefab;
        }

        switch (factory.Level)
        {
            case 1:
                return _factoryLevel1Prefab;
            case 2:
                return _factoryLevel2Prefab;
            case 3:
            default:
                return _factoryLevel3Prefab;
        }
    }
}
