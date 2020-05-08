using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonBase<MapManager>
{
    public GameObject[,] Hexs { get; set; }

    public GameObject Hex(Vector2Int position)
    {
        return Hexs[position.x, position.y];
    }
}
