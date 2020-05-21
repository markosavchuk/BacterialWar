using UnityEngine;

//todo make it base for all other map objects
public class MapObjectComponent : MonoBehaviour
{
    public Vector2Int MapPosition;
    public bool IsInMotion;
    public Player Player;
    public bool CanMove;
}
