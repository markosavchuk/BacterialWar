using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo rename it to something like MobMovement
public class MobSimulator : MonoBehaviour
{
    private MapObjectComponent _mapObjectComponent;

    private void Start()
    {
        _mapObjectComponent = GetComponent<MapObjectComponent>();
    }

    void Update()
    {
        MoveMob();
    }

    private void MoveMob()
    {
        if (_mapObjectComponent.IsInMotion || !_mapObjectComponent.CanMove)
        {
            return;
        }

        var newPosition = NavigationManager.Instance.FindNewPlaceForMob(
            _mapObjectComponent.MapPosition,
            _mapObjectComponent.Player);

        NavigationManager.Instance.PutGameObjectOnHex(gameObject, newPosition);       
    }
}
