using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSimulator : MonoBehaviour
{
    //todo move this value to another place;
    private float _movePeriod = 0f;

    private float _time = 0f;
    private Vector2Int _mapPosition;
    private MapObjectComponent _mapObjectComponent;

    private void Start()
    {
        _mapObjectComponent = GetComponent<MapObjectComponent>();
        _mapPosition = _mapObjectComponent.MapPosition;
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

        _mapPosition = NavigationManager.Instance.FindNewPlaceForMob(_mapPosition);
        NavigationManager.Instance.PutGameObjectOnHex(gameObject, _mapPosition);       
    }
}
