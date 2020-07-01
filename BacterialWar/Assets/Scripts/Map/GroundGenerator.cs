using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    private const float _hexWidth = 1.004425f;
    private const float _hexHeight = 1.15404f;

    private float _hexWidthWithGap;
    private float _hexHeightWithGap;
    private Vector3 _startPos;

    [SerializeField]
    private GameObject _battleHexPrefab;

    [SerializeField]
    private GameObject _factory1HexPrefab;

    [SerializeField]
    private GameObject _factory2HexPrefab;

    [SerializeField]
    private int _gridWidth;

    [SerializeField]
    private int _gridHeight;

    [SerializeField]
    private int _factoryHeight;

    [SerializeField]
    private float _gap;

    private void Awake()
    {
        if (_gridHeight % 2 != 0)
        {
            throw new ArgumentException($"{nameof(_gridHeight)} should be even number.");
        }
    }

    private void Start()
    {
        MapManager.Instance.Hexs = new HexObject[_gridWidth, _gridHeight];

        CalculateGap();
        CalculateStartPosition();
        CreateGrid();
    }

    private void CalculateGap()
    {
        _hexWidthWithGap = _hexWidth * (1+_gap);
        _hexHeightWithGap = _hexHeight * (1+_gap);

        MapManager.Instance.HexRadius = _hexWidthWithGap / 2;
    }

    private void CalculateStartPosition()
    {
        var centerPosition = transform.position;

        float offsetX = centerPosition.x;

        if (_gridHeight / 2 % 2 != 0)
        {
            offsetX += _hexWidthWithGap / 2;
        }

        float x = -_hexWidthWithGap * (_gridWidth / 2) - offsetX;
        float z = _hexHeightWithGap * 0.75f * (_gridHeight / 2) - centerPosition.z;

        _startPos = new Vector3(x, centerPosition.y, z);
    }

    private Vector3 CalculateHexPosition(int x, int y)
    {
        float offset = 0;
        if (y % 2 != 0)
            offset = _hexWidthWithGap / 2;

        float posX = _startPos.x + x * _hexWidthWithGap + offset;
        float posZ = _startPos.z - y * _hexHeightWithGap * 0.75f;

        return new Vector3(posX, _startPos.y, posZ);
    }

    private void CreateGrid()
    {
        for (int y = 0; y < _gridHeight; y++)
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                var hexType = y < _factoryHeight || y > _gridHeight - _factoryHeight - 1
                    ? HexType.Factory
                    : HexType.Battle;

                var hexObject = hexType == HexType.Factory
                    ? y < _gridHeight / 2 ? Instantiate(_factory1HexPrefab) : Instantiate(_factory2HexPrefab)
                    : Instantiate(_battleHexPrefab);

                hexObject.transform.position = CalculateHexPosition(x, y);
                hexObject.transform.parent = this.transform;
                hexObject.name = $"Hexagon[{x},{y}]";

                var hexComponent = hexObject.AddComponent<HexObject>() as HexObject;
                hexComponent.HexType = hexType;
                hexComponent.MapPosition = new Vector2Int(x, y);
                hexComponent.Player = GetPlayerForFactoryNode(hexComponent.MapPosition);

                MapManager.Instance.Hexs[x, y] = hexComponent;
            }
        }
    }

    private Player GetPlayerForFactoryNode(Vector2Int position)
    {
        return position.y < MapManager.Instance.Height / 2 ? Player.Player2 : Player.Player1;
    }
}
