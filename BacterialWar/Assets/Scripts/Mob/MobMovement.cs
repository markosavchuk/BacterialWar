using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    private MobObject _mobObject;

    //todo serialize
    private Vector3 _mobOffset = new Vector3(0, 0.5f, 0);   
    private float _speed = 4f;

    private bool _isInMotion;
    private float _realSpped;

    private void Awake()
    {
        _mobObject = GetComponent<MobObject>();

        if (_mobObject == null)
        {
            //todo stop script
        }

        _realSpped = _speed * Settings.Instance.StepTime;
    }

    void Update()
    {
        if (_isInMotion)
        {
            Move();
        }
        else if (!_mobObject.IsFrozen)
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

        _isInMotion = true;

        var oldHexContainer = _mobObject.ParentHex;
        var newHexContainer = MapManager.Instance.Hex(newPlace.Value);
        newHexContainer.SetContent(_mobObject, oldHexContainer);

        Move();
    }

    private void Move()
    {
        var targetPosition = MapManager.Instance.Hex(_mobObject.MapPosition).transform.position + _mobOffset;

        if (Vector3.Distance(gameObject.transform.position, targetPosition) < 0.2f)
        {
            gameObject.transform.position = targetPosition;
            _isInMotion = false;
        }
        else
        {
            var dir = targetPosition - gameObject.transform.position;
            gameObject.transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);
        }
    }
}
