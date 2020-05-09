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
    private float _speed = 3f;
    private Vector3 _mobOffset = new Vector3(0, 0.5f, 0);

    private List<GameObject> _movingObjects = new List<GameObject>();

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
        var mapObjectComponent = gameObject.GetComponent<MapObjectComponent>();

        // Check if it's possible
        if (!IsAvailable(
                position,
                gameObject.GetComponent<FactoryComponent>() != null
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

        // Set ObjectAbove in hexComponent
        if (oldPosition != Vector2Int.zero &&
            MapManager.Instance.Hex(oldPosition).GetComponent<HexComponent>() is HexComponent oldHexComponent)
        {
            oldHexComponent.ObjectAbove = null;
        }

        if (MapManager.Instance.Hex(position).GetComponent<HexComponent>() is HexComponent hexComponent)
        {
            hexComponent.ObjectAbove = gameObject;
        }        

        return true;
    }

    public bool IsAvailable(Vector2Int position, HexType type, Player player)
    {
        // Check if Map has this Hex position
        if (position.x<0 || MapManager.Instance.Hexs.GetLength(0) <= position.x ||
            position.y<=0 || MapManager.Instance.Hexs.GetLength(1) <= position.y)
        {
            return false;
        }

        if (!(MapManager.Instance.Hex(position).GetComponent<HexComponent>() is HexComponent hexComponent))
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

        // Check if game state allow it
        if (StateManager.Instance.GameState == GameState.Preparation)
        {
            if ((player == Player.Player1 && position.y < MapManager.Instance.Hexs.GetLength(1) / 2) ||
                (player == Player.Player2 && position.y >= MapManager.Instance.Hexs.GetLength(1) / 2))
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
            var mapObjectComponent = gameObject.GetComponent<MapObjectComponent>();
            var mapPosition = mapObjectComponent.MapPosition;
            var targetPosition = MapManager.Instance.Hex(mapPosition).transform.position + ObjectOffset(gameObject);

            if (Vector3.Distance(gameObject.transform.position, targetPosition) < 0.2f)
            {
                gameObject.transform.position = targetPosition ;
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

    private Vector3 ObjectOffset(GameObject gameObject)
    {
        if (gameObject.GetComponent<MobComponent>() != null)
        {
            return _mobOffset;
        }
        else
        {
            return Vector3.zero;
        }
    }
}