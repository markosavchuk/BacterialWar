using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonBase<MapManager>
{
    public GameObject[,] Hexs { get; set; }
}
