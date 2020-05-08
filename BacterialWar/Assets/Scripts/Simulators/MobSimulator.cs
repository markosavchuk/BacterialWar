using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSimulator : MonoBehaviour
{
    //todo move this value to another place;
    [SerializeField]
    private float movePeriod = 1f;

    private float _time = 0f;
    private Vector2Int _mapPosition;

    private void Start()
    {
        if (GetComponent<MapObjectComponent>() is MapObjectComponent mapObjectComponent)
        {
            _mapPosition = mapObjectComponent.MapPosition;
        }
    }

    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= movePeriod)
        {
            _time -= movePeriod;

            MoveMob();
        }
    }

    private void MoveMob()
    {
        _mapPosition = NavigationManager.Instance.FindNewPlaceForMob(_mapPosition);
        NavigationManager.Instance.PutGameObjectOnHex(gameObject, _mapPosition);       
    }
}
