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
    private GameObject battleHexPrefab;

    [SerializeField]
    private GameObject factoryHexPrefab;

    [SerializeField]
    private int gridWidth;

    //todo make sure it's gridHeight%2==0
    [SerializeField]
    private int gridHeight;

    [SerializeField]
    private int factoryHeight;

    [SerializeField]
    private float gap;  
 
    private void Start()
    {
        MapManager.Instance.Hexs = new GameObject[gridWidth, gridHeight];

        CalculateGap();
        CalculateStartPosition();
        CreateGrid();

        CreateFactories();
    }

    private void CalculateGap()
    {
        _hexWidthWithGap = _hexWidth * (1+gap);
        _hexHeightWithGap = _hexHeight * (1+gap);
    }

    private void CalculateStartPosition()
    {
        var centerPosition = transform.position;

        float offsetX = centerPosition.x;

        if (gridHeight / 2 % 2 != 0)
        {
            offsetX += _hexWidthWithGap / 2;
        }

        float x = -_hexWidthWithGap * (gridWidth / 2) - offsetX;
        float z = _hexHeightWithGap * 0.75f * (gridHeight / 2) - centerPosition.z;

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
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                var hexType = y < factoryHeight || y > gridHeight - factoryHeight - 1
                    ? HexType.Factory
                    : HexType.Battle;

                var hexObject = hexType == HexType.Factory
                    ? Instantiate(factoryHexPrefab)
                    : Instantiate(battleHexPrefab);

                hexObject.transform.position = CalculateHexPosition(x, y);
                hexObject.transform.parent = this.transform;
                hexObject.name = $"Hexagon[{x},{y}]";

                var hexComponent = hexObject.AddComponent<HexComponent>() as HexComponent;

                hexComponent.HexType = hexType;

                MapManager.Instance.Hexs[x, y] = hexObject;
            }
        }
    }

    //todo move it to another place later
    private void CreateFactories()
    {
        FactoryManager.Instance.CreateFactory(new Vector2Int(1, 10), Player.Player1);
        FactoryManager.Instance.CreateFactory(new Vector2Int(9, 10), Player.Player1);

        FactoryManager.Instance.CreateFactory(new Vector2Int(3, 1), Player.Player2);
        FactoryManager.Instance.CreateFactory(new Vector2Int(7, 1), Player.Player2);
    }
}
