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

    public GameObject GetFactoryPrefab(FactoryObject factory = null)
    {
        if (factory == null)
        {
            return _factoryLevel1Prefab;
        }

        switch (factory)
        {
            case var _ when factory.Level == 1:
                return _factoryLevel1Prefab;
            case var _ when factory.Level == 2 && factory.Player == Player.Player1:
                return _factoryLevel2Player1Prefab;
            case var _ when factory.Level == 2 && factory.Player == Player.Player2:
                return _factoryLevel2Player2Prefab;
            case var _ when factory.Level >= 3 && factory.Player == Player.Player1:
                return _factoryLevel3Player1Prefab;
            case var _ when factory.Level >= 3 && factory.Player == Player.Player1:
                return _factoryLevel3Player2Prefab;
            default:
                return null;
        }
    }
}
