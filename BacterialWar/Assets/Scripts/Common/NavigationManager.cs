using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

//todo reg rid of it
public class NavigationManager : SingletonMonoBehaviour<NavigationManager>
{
    //todo move this to another place
    /*private float _speedK = 4f;

    private float _speed;
    private Vector3 _mobOffset = new Vector3(0, 0.5f, 0);
    private Vector3 _factoryOffset = new Vector3(0, 0.24f, 0);

    private List<GameObject> _movingObjects = new List<GameObject>();

    private void Start()
    {
        _speed = _speedK * Settings.Instance.StepTime;
    }

    private void Update()
    {
        MoveObjects();
    }

    public Vector2Int FindNewPlaceForMob(Vector2Int oldPlace, Player player)
    {
        var delta = oldPlace.y % 2 == 0 ? -1 : 1;
        var direction = player == Player.Player1 ? 1 : -1;

        var path1 = new Vector2Int(oldPlace.x, oldPlace.y - 1 * direction);
        var path2 = new Vector2Int(oldPlace.x + delta, oldPlace.y - 1 * direction);

        if (IsAvailable(path1, HexType.Battle, player) && IsAvailable(path2, HexType.Battle, player))
        {
            var randomPath = UnityEngine.Random.Range(0, 2);
            return randomPath == 0 ? path1 : path2;

        }
        else if (IsAvailable(path1, HexType.Battle, player))
        {
            return path1;
        }
        else if (IsAvailable(path2, HexType.Battle, player))
        {
            return path2;
        }
        else
        {
            return oldPlace;
        }
    }

    public bool PutGameObjectOnHex(GameObject gameObject, Vector2Int position)
    {
        var mapObjectComponent = gameObject.GetComponent<MapObject>();

        // Check if it's possible
        if (!IsAvailable(
                position,
                gameObject.GetComponent<FactoryObject>() != null
                    ? HexType.Factory
                    : HexType.Battle,
                mapObjectComponent.Player))
        {
            return false;
        }

        // Check if object already in motion
        if (mapObjectComponent.IsInMotion)
        {
            return false;
        }

        // Set Map Position to mapObjectComponent
        Vector2Int oldPosition = Vector2Int.zero;

        if (mapObjectComponent.MapPosition != Vector2Int.zero)
        {
            oldPosition = mapObjectComponent.MapPosition;
        }

        mapObjectComponent.MapPosition = position;

        // Set global position to GameObject or start animation moving        
        if (gameObject.transform.position != Vector3.zero)
        {
            mapObjectComponent.IsInMotion = true;
            _movingObjects.Add(gameObject);
        }
        else
        {
            var newGlobalPosition = MapManager.Instance.Hex(position).transform.position;
            gameObject.transform.position = newGlobalPosition + ObjectOffset(gameObject);
        }

        // Set ObjectAbove in hexComponent or Factory
        if (oldPosition != Vector2Int.zero)
        {
            var oldHexComponent = MapManager.Instance.Hex(oldPosition).GetComponent<HexObject>();
            if (oldHexComponent.HexType == HexType.Factory)
            {
                if (oldHexComponent.Container != null &&
                    oldHexComponent.Container.GetComponent<FactoryObject>() is FactoryObject factoryComponent)
                {
                    factoryComponent.MobAbove = null;
                }
            }
            else if (oldHexComponent.HexType == HexType.Battle)
            {
                oldHexComponent.Container = null;
            }
        }

        if (MapManager.Instance.Hex(position).GetComponent<HexObject>() is HexObject hexComponent)
        {
            hexComponent.Container = gameObject;
        }

        return true;
    }

    public bool IsAvailable(Vector2Int position, HexType type, Player player)
    {
        // Check if Map has this Hex position
        if (position.x < 0 || MapManager.Instance.Width <= position.x ||
            position.y < 0 || MapManager.Instance.Height <= position.y)
        {
            return false;
        }

        if (!(MapManager.Instance.Hex(position).GetComponent<HexObject>() is HexObject hexComponent))
        {
            return false;
        }

        // Check if it's allowed to put this type of object
        var typeOfHex = hexComponent.HexType;
        if (typeOfHex != type)
        {
            return false;
        }

        // Check if position is empty
        if (hexComponent.Container != null)
        {
            return false;
        }

        // Check if game state allow it
        if (StateManager.Instance.GameState == GameState.Preparation)
        {
            if ((player == Player.Player1 && position.y < MapManager.Instance.Height / 2) ||
                (player == Player.Player2 && position.y >= MapManager.Instance.Height / 2))
            {
                return false;
            }
        }

        return true;
    }

    private void MoveObjects()
    {
        foreach (var gameObject in _movingObjects)
        {
            // Object is already destroyed.
            if (gameObject == null)
            {
                continue;
            }

            var mapObjectComponent = gameObject.GetComponent<MapObject>();
            var mapPosition = mapObjectComponent.MapPosition;
            var targetPosition = MapManager.Instance.Hex(mapPosition).transform.position + ObjectOffset(gameObject);

            if (Vector3.Distance(gameObject.transform.position, targetPosition) < 0.2f)
            {
                gameObject.transform.position = targetPosition;
                mapObjectComponent.IsInMotion = false;
            }
            else
            {
                var dir = targetPosition - gameObject.transform.position;
                gameObject.transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);
            }
        }

        _movingObjects.RemoveAll(g => g == null || !g.GetComponent<MapObject>().IsInMotion);
    }

    private Vector3 ObjectOffset(GameObject gameObject)
    {
        if (gameObject.GetComponent<MobObject>() != null)
        {
            return _mobOffset;
        }
        else if (gameObject.GetComponent<FactoryObject>() != null)
        {
            return _factoryOffset;
        }
        else
        {
            return Vector3.zero;
        }
    }*/
}