using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    private MobObject _mobObject;

    [SerializeField]
    private Vector3 _mobOffset = new Vector3(0, 0.5f, 0);

    [SerializeField]
    private float _speed = 6f;

    private float _realSpeed;

    private void Awake()
    {
        _mobObject = GetComponent<MobObject>();
        _realSpeed = _speed * Settings.Instance.StepTime;
    }

    void Update()
    {
        if (_mobObject.IsInMotion)
        {
            Move();
        }
        else if (_mobObject.FrozenMovement == 0)
        {
            MakeNextStep();
        }
    }

    private void MakeNextStep()
    {
        var newPlace = MobMovementExtension.FindNewPlaceForMob(
            _mobObject.MapPosition,
            _mobObject.Player);

        if (!newPlace.HasValue)
        {
            return;
        }

        _mobObject.IsInMotion = true;

        if (_mobObject.OnFactory != null)
        {
            _mobObject.OnFactory.SetMobAbove(null);
        }

        var oldHexContainer = _mobObject.ParentHex;
        var newHexContainer = MapManager.Instance.Hex(newPlace.Value);
        newHexContainer.SetContent(_mobObject, oldHexContainer);

        ChangeNodePlayer(newPlace.Value);

        Move();
    }

    private void Move()
    {
        var targetPosition = MapManager.Instance.Hex(_mobObject.MapPosition).transform.position + _mobOffset;

        if (Vector3.Distance(gameObject.transform.position, targetPosition) < 0.2f)
        {
            gameObject.transform.position = targetPosition;
            _mobObject.IsInMotion = false;
        }
        else
        {
            var dir = targetPosition - gameObject.transform.position;
            gameObject.transform.Translate(dir.normalized * _realSpeed * Time.deltaTime, Space.World);
        }
    }

    private void ChangeNodePlayer(Vector2Int position)
    {
        MapManager.Instance.Hex(position).Player = _mobObject.Player;
    }
}
