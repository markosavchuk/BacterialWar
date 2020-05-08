using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    //todo make helper class for it
    #region Singleton logic    

    public static NavigationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    //todo move this to another place
    private float _speed = 3;

    private List<GameObject> _movingObjects = new List<GameObject>();

    private void Update()
    {
        MoveObjects();
    }

    public Vector2Int FindNewPlaceForMob(Vector2Int oldPlace)
    {
        //todo check if there available place for mob
        //todo find place only with proper type
        //todo improve searching

        return new Vector2Int(oldPlace.x, oldPlace.y - 1);
    }

    public bool PutGameObjectOnHex(GameObject gameObject, Vector2Int position)
    {
        // Check if it's possible
        if (!IsAvailable(
                position,
                gameObject.GetComponent<FactoryComponent>() != null
                    ? HexType.Factory
                    : HexType.Battle))
        {
            return false;
        }

        var mapObjectComponent = gameObject.GetComponent<MapObjectComponent>();

        // Check if object already in motion
        if (mapObjectComponent!=null && mapObjectComponent.IsInMotion)
        {
            return false;
        }

        // Set Map Position to mapObjectComponent
        Vector2Int oldPosition = Vector2Int.zero;
        
        if (mapObjectComponent != null && mapObjectComponent.MapPosition != Vector2Int.zero)
        {
            oldPosition = mapObjectComponent.MapPosition;
        }
        else if (mapObjectComponent == null)
        {
            mapObjectComponent = gameObject.AddComponent<MapObjectComponent>();
        }

        mapObjectComponent.MapPosition = position;

        // Set global position to GameObject or start animation moving        
        if (oldPosition != Vector2Int.zero)
        {
            mapObjectComponent.IsInMotion = true;
            _movingObjects.Add(gameObject);
        }
        else
        {
            var newGlobalPosition = MapManager.Instance.Hexs[position.x, position.y].transform.position;
            gameObject.transform.position = newGlobalPosition;
        }

        // Set ObjectAbove in hexComponent
        if (oldPosition != Vector2Int.zero &&
            MapManager.Instance.Hexs[oldPosition.x, oldPosition.y].GetComponent<HexComponent>() is HexComponent oldHexComponent)
        {
            oldHexComponent.ObjectAbove = null;
        }

        if (MapManager.Instance.Hexs[position.x, position.y].GetComponent<HexComponent>() is HexComponent hexComponent)
        {
            hexComponent.ObjectAbove = gameObject;
        }        

        return true;
    }

    public bool IsAvailable(Vector2Int position, HexType type)
    {
        // Check if Map has this Hex position
        if (position.x<0 || MapManager.Instance.Hexs.GetLength(0) <= position.x ||
            position.y<=0 || MapManager.Instance.Hexs.GetLength(1) <= position.y)
        {
            return false;
        }

        //todo fix crash which heppens here sometimes
        if (!(MapManager.Instance.Hexs[position.x, position.y].GetComponent<HexComponent>() is HexComponent hexComponent))
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
        if (hexComponent.ObjectAbove != null)
        {
            return false;
        }

        return true;
    }

    private void MoveObjects()
    {
        foreach (var gameObject in _movingObjects)
        {
            var mapObjectComponent = gameObject.GetComponent<MapObjectComponent>();
            var mapPosition = mapObjectComponent.MapPosition;
            var targetPosition = MapManager.Instance.Hexs[mapPosition.x, mapPosition.y].transform.position;

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

        _movingObjects.RemoveAll(g => !g.GetComponent<MapObjectComponent>().IsInMotion);
    }
}