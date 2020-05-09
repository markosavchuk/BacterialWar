using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSimulator : MonoBehaviour
{
    //todo move this value to another place;
    private float _movePeriod = 0f;

    private float _time = 0f;
    private MapObjectComponent _mapObjectComponent;

    private void Start()
    {
        _mapObjectComponent = GetComponent<MapObjectComponent>();
    }

    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _movePeriod)
        {
            _time -= _movePeriod;

            MoveMob();
        }
    }

    private void MoveMob()
    {
        if (_mapObjectComponent.IsInMotion)
        {
            return;
        }

        var newPosition = NavigationManager.Instance.FindNewPlaceForMob(
            _mapObjectComponent.MapPosition,
            _mapObjectComponent.Player);

        NavigationManager.Instance.PutGameObjectOnHex(gameObject, newPosition);       
    }
}
