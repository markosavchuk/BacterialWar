using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonBase<MapManager>
{
    public int Width;
    public int Height;

    private HexObject[,] _hexs;
    public HexObject[,] Hexs
    {
        get => _hexs;
        set
        {
            _hexs = value;

            Width = _hexs.GetLength(0);
            Height = _hexs.GetLength(1);
        }
    }

    public HexObject Hex(Vector2Int position)
    {
        return Hexs[position.x, position.y];
    }

    public Player GetPlayerForFactoryNode(Vector2Int position)
    {
        return position.y + 1 < Height / 2 ? Player.Player2 : Player.Player1;
    }

    public bool IsExist(Vector2Int position)
    {
        return position.x >= 0 && position.x < Width &&
            position.y >= 0 && position.y < Height;
    }
}
