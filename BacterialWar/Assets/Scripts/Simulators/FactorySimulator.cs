﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactorySimulator : MonoBehaviour
{
    [SerializeField]
    private GameObject mob;

    //todo move this value to another place;
    [SerializeField]
    private float reproducablePeriod = 1f;

    private float _time = 0f;
    private Vector2Int _mapPosition;

    private void Start()
    {
        if (GetComponent<MapObjectComponent>() is MapObjectComponent mapObjectComponent){
            _mapPosition = mapObjectComponent.MapPosition;
        }
    }

    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= reproducablePeriod)
        {
            _time -= reproducablePeriod;

            MakeNewMob();
        }
    }

    private void MakeNewMob()
    {
        var mobPosition = NavigationManager.Instance.FindNewPlaceForMob(_mapPosition);
        if (!NavigationManager.Instance.IsAvailable(mobPosition, HexType.Battle))
        {
            return;
        }

        var newMob = Instantiate(mob);
        var mobComponent = newMob.AddComponent<MobComponent>();

        //todo make mob start position in factory so it will move from factory
        NavigationManager.Instance.PutGameObjectOnHex(newMob, mobPosition);
    }
}