using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonBase<MapManager>
{
    //todo implement access viw Vector2Int

    public GameObject[,] Hexs { get; set; }

    public void PutObjectAboveHex(Vector2Int position, GameObject @object)
    {

    }
}
