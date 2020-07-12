using UnityEngine;
using System.Collections;

public class BattleAreaFactoryBuilder : MonoBehaviour, IBuilder
{
    [SerializeField]
    private GameObject _factoryLevel1Prefab;

    [SerializeField]
    private GameObject _factoryLevel2Player1Prefab;

    [SerializeField]
    private GameObject _factoryLevel2Player2Prefab;

    [SerializeField]
    private GameObject _factoryLevel3Player1Prefab;

    [SerializeField]
    private GameObject _factoryLevel3Player2Prefab;

    public GameObject GetStartFactoryPrefab()
    {
        return _factoryLevel1Prefab;
    }
}
