using UnityEngine;

public class NavigationManager : SingletonBase<NavigationManager>
{
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

        // Set Map Position to mapObjectComponent
        Vector2Int oldPosition = Vector2Int.zero;

        var mapObjectComponent = gameObject.GetComponent<MapObjectComponent>();
        if (mapObjectComponent != null && mapObjectComponent.MapPosition != Vector2Int.zero)
        {
            oldPosition = mapObjectComponent.MapPosition;
        }
        else if (mapObjectComponent == null)
        {
            mapObjectComponent = gameObject.AddComponent<MapObjectComponent>();
        }

        mapObjectComponent.MapPosition = position;

        // Set global position to GameObject
        if (oldPosition != Vector2Int.zero)
        {
            //todo add animation
            gameObject.transform.position = MapManager.Instance.Hexs[position.x, position.y].transform.position;
        }
        else
        { 
            gameObject.transform.position = MapManager.Instance.Hexs[position.x, position.y].transform.position;
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
        if (MapManager.Instance.Hexs.GetLength(0) <= position.x ||
            MapManager.Instance.Hexs.GetLength(1) <= position.y)
        {
            return false;
        }

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
}
