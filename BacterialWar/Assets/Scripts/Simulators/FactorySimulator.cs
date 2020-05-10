using System.Collections;
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
    private MapObjectComponent _mapObjectComponent;

    private void Start()
    {
        _mapObjectComponent = GetComponent<MapObjectComponent>();

        //todo find better way
        mobsParant = GameObject.Find("Mobs")?.transform;
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
        var mobPosition = NavigationManager.Instance.FindNewPlaceForMob(
            _mapObjectComponent.MapPosition,
            _mapObjectComponent.Player);

        if (!NavigationManager.Instance.IsAvailable(
                mobPosition,
                HexType.Battle,
                _mapObjectComponent.Player))
        {
            return;
        }

        var newMob = Instantiate(mob);
        newMob.transform.parent = mobsParant.transform;
        newMob.tag = TagConstants.Mob;

        var mapObjectComponent = newMob.AddComponent<MapObjectComponent>();
        mapObjectComponent.Player = _mapObjectComponent.Player;

        newMob.GetComponent<MeshRenderer>().material.color =
            _mapObjectComponent.Player == Player.Player1
                ? Color.white
                : Color.black;

        var mobComponent = newMob.AddComponent<MobComponent>();

        newMob.transform.position = MapManager.Instance.Hex(_mapObjectComponent.MapPosition).transform.position;

        NavigationManager.Instance.PutGameObjectOnHex(newMob, mobPosition);
    }
}
