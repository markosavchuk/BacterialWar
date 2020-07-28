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

    private int _gridWidth;
    private int _gridHeight;
    private int _factoryHeight;

    [SerializeField]
    private GameObject _battleHexPrefab;

    [SerializeField]
    private GameObject _factory1HexPrefab;

    [SerializeField]
    private GameObject _factory2HexPrefab;

    [SerializeField]
    private float _gap;

    [SerializeField]
    private float _horizontalHexMarginPersentage;

    [SerializeField]
    private float _verticalHexMarginPersentage;

    [SerializeField]
    private int _maxWidth;

    private void Awake()
    {
        if (_gridHeight % 2 != 0)
        {
            throw new ArgumentException($"{nameof(_gridHeight)} should be even number.");
        }

        CalculateGap();
        CaclulateWidthAndHeight();
        CalculateStartPosition();
        CreateGrid();
    }

    private void CaclulateWidthAndHeight()
    {
        var hexRectangle = VectorHelper.GUIRectWithObject(_battleHexPrefab);

        var marginHorizontal = hexRectangle.height * _horizontalHexMarginPersentage;
        var marginVertical = hexRectangle.width * _verticalHexMarginPersentage;

        // Calculate width
        _gridWidth = (int)((Screen.width - (marginHorizontal * 2)) / (hexRectangle.width * (1 + _gap)));
        if (_gridWidth > _maxWidth)
        {
            _gridWidth = _maxWidth;
        }

        // Calculate height
        double height = (Screen.height - (marginVertical * 2)) / (hexRectangle.height * (1 + _gap));
        int roundedHeight = (int)height;

        if (roundedHeight % 2 != 0 && height > roundedHeight)
        {
            roundedHeight++;
        }
        else if (roundedHeight % 2 != 0) 
        {
            roundedHeight--;
        }

        _gridHeight = roundedHeight;

        _factoryHeight = _gridHeight >= 12 ? 2 : 1;

        // Offeset camera X position if needed
        if (_gridWidth % 2 == 0)
        {
            var cameraPos = Camera.main.transform.position;
            Camera.main.transform.position = new Vector3(
                cameraPos.x - _hexWidth / 2,
                cameraPos.y,
                cameraPos.z);
        }

        MapManager.Instance.Hexs = new HexObject[_gridWidth, _gridHeight];
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

                if (hexType == HexType.Factory)
                {
                    hexObject.AddComponent<FactoryNodeInteraction>();
                }

                MapManager.Instance.Hexs[x, y] = hexComponent;
            }
        }
    }

    private Player GetPlayerForFactoryNode(Vector2Int position)
    {
        return position.y < MapManager.Instance.Height / 2 ? Player.EnemyPlayer : Player.MyPlayer;
    }
}
