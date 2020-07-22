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
    private float _speed = 3f;

    private float _realSpeed;
    private float? minDistanseToMove = null;

    private void Awake()
    {
        _mobObject = GetComponent<MobObject>();
        _realSpeed = _speed / Settings.Instance.StepTime;
    }

    private BaseMobAttacker _mobAttacker;
    private BaseMobAttacker MobAttacker
    {
        get
        {
            if (_mobAttacker == null)
            {
                _mobAttacker = gameObject.GetComponent<BaseMobAttacker>();
            }
            return _mobAttacker;
        }
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

        if (MobAttacker!=null && !MobAttacker.ShouldMove())
        {
            return;
        }

        _mobObject.IsInMotion = true;

        if (_mobObject.OnFactory != null)
        {
            _mobObject.OnFactory.SetMobAbove(null);
        }

        var newHexContainer = MapManager.Instance.Hex(newPlace.Value);

        // Mob is going to battle hex
        if (newHexContainer.HexType == HexType.Battle)
        {
            var oldHexContainer = _mobObject.ParentHex;
            newHexContainer.SetContent(_mobObject, oldHexContainer);

            ChangeNodePlayer(newPlace.Value);
        }
        // Mob is going to my factory hex
        else if (newHexContainer.HexType == HexType.Factory && newHexContainer.Player == _mobObject.Player)
        {
            // Mob is going to another factory
            if (newHexContainer.Content != null && newHexContainer.Content is FactoryObject newParantFactory)
            {
                newParantFactory.SetMobAbove(_mobObject);
            }
            // Mob is going to empty factory hex
            else
            {
                newHexContainer.SetContent(_mobObject);
            }
        }

        MobAttacker?.ResetWaitingTime();

        Move();
    }

    
    private void Move()
    {
        var targetPosition = MapManager.Instance.Hex(_mobObject.MapPosition).transform.position + _mobOffset;        

        if (minDistanseToMove.HasValue && Vector3.Distance(gameObject.transform.position, targetPosition) < minDistanseToMove)
        {
            gameObject.transform.position = targetPosition;
            _mobObject.IsInMotion = false;
        }
        else
        {
            var dir = targetPosition - gameObject.transform.position;
            var translation = dir.normalized * _realSpeed * Time.deltaTime;

            if (!minDistanseToMove.HasValue)
            {
                minDistanseToMove = Vector3.Magnitude(translation) * 1.5f;
            }

            gameObject.transform.Translate(translation, Space.World);
        }
    }

    private void ChangeNodePlayer(Vector2Int position)
    {
        MapManager.Instance.Hex(position).Player = _mobObject.Player;
    }
}
