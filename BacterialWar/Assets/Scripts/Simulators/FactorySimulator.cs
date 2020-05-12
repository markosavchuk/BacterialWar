﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactorySimulator : MonoBehaviour
{
    [SerializeField]
    private GameObject mob;

    [SerializeField]
    private Transform mobsParant;

    //todo move this value to another place;
    [SerializeField]
    private float reproducablePeriod;

    private float _time = 0f;
    private float _reproducableTime;
    private MapObjectComponent _mapObjectComponent;
    private FactoryComponent _factoryComponent;

    private void Start()
    {
        _mapObjectComponent = GetComponent<MapObjectComponent>();
        _factoryComponent = GetComponent<FactoryComponent>();

        _reproducableTime = reproducablePeriod * Settings.StepTime;

        //todo find better way
        mobsParant = GameObject.Find("Mobs")?.transform;
    }

    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _reproducableTime)
        {
            _time -= _reproducableTime;

            MakeNewMob();
        }
    }

    private void MakeNewMob()
    {
        if (_factoryComponent.MobAbove != null)
        {
            return;
        }

        var newMob = Instantiate(mob);
        newMob.transform.parent = mobsParant.transform;
        newMob.tag = TagConstants.Mob;

        var mapObjectComponent = newMob.AddComponent<MapObjectComponent>();
        mapObjectComponent.Player = _mapObjectComponent.Player;
        mapObjectComponent.CanMove = true;
        mapObjectComponent.MapPosition = _mapObjectComponent.MapPosition;

        newMob.GetComponent<MeshRenderer>().material.color =
            _mapObjectComponent.Player == Player.Player1
                ? Color.white
                : Color.black;

        var mobComponent = newMob.AddComponent<MobComponent>();
        mobComponent.Damage = _mapObjectComponent.Player == Player.Player1 ? 25 : 10;

        newMob.transform.position = MapManager.Instance.Hex(_mapObjectComponent.MapPosition).transform.position;

        _factoryComponent.MobAbove = newMob;
    }
}
