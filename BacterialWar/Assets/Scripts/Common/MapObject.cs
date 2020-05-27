using System;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public EventHandler PlayerChanged;

    public Vector2Int MapPosition { get; set; }

    private Player _player;
    public Player Player
    {
        get => _player;
        set
        {
            _player = value;
            PlayerChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
