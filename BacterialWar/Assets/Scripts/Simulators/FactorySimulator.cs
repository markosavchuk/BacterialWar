using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo refactor solution to not attach this script manually to all factories
//todo rename it
public class FactorySimulator : MonoBehaviour
{
    [SerializeField]
    private GameObject mob1;

    [SerializeField]
    private GameObject mob2;

    //[SerializeField]
    private Transform mobsParant;

    //todo move this value to another place;
    [SerializeField]
    private float reproducablePeriod = 3;

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

        var newMob = Instantiate(_mapObjectComponent.Player == Player.Player1 ? mob1 : mob2);
        newMob.transform.parent = mobsParant.transform;
        newMob.tag = TagConstants.Mob;

        var mapObjectComponent = newMob.AddComponent<MapObjectComponent>();
        mapObjectComponent.Player = _mapObjectComponent.Player;
        mapObjectComponent.CanMove = true;
        mapObjectComponent.MapPosition = _mapObjectComponent.MapPosition;

        /*newMob.GetComponent<MeshRenderer>().material.color =
            _mapObjectComponent.Player == Player.Player1
                ? Color.white
                : Color.black;*/

        var mobComponent = newMob.AddComponent<MobComponent>();
        mobComponent.Damage = _mapObjectComponent.Player == Player.Player1 ? 25 : 10;

        newMob.transform.position = MapManager.Instance.Hex(_mapObjectComponent.MapPosition).transform.position;

        _factoryComponent.MobAbove = newMob;
    }
}
