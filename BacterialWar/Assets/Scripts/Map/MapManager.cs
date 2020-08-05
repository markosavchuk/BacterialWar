using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapManager : SingletonBase<MapManager>
{
    public int Width;
    public int Height;

    public float HexRadius;

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

    public bool IsExist(Vector2Int position)
    {
        return position.x >= 0 && position.x < Width &&
            position.y >= 0 && position.y < Height;
    }

    public List<HexObject> GetAllFactoryHexs(Player player)
    {
        var hexs = new List<HexObject>();
        for (var i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                var hex = Hexs[i, j];
                if (hex.Player == player && hex.HexType == HexType.Factory)
                {
                    hexs.Add(hex);
                }
            }
        }
        return hexs;
    }

    public CrystalObject GetCrystal(Player player)
    {
        var t = GameObject.FindObjectsOfType<CrystalObject>().ToList();
        return GameObject.FindObjectsOfType<CrystalObject>().Where(c => c.Player == player).FirstOrDefault();
    }
}
