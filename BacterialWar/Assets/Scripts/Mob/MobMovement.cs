using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo rename it to something like MobMovement
public class MobMovement : MonoBehaviour
{
    /*private MapObject  _mapObjectComponent;

    private void Start()
    {
        _mapObjectComponent = GetComponent<MapObject>();
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
    }*/
}
