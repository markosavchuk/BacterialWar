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
    private GameObject factory1HexPrefab;

    [SerializeField]
    private GameObject factory2HexPrefab;

    //todo move it to another place
    #region temporary SerializeField
    [SerializeField]
    private GameObject crystal1Prefab;

    [SerializeField]
    private GameObject crystal2Prefab;
    #endregion

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

        AddStartObjects();
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
                    ? y < gridHeight / 2 ? Instantiate(factory1HexPrefab) : Instantiate(factory2HexPrefab)
                    : Instantiate(battleHexPrefab);

                //var mesh = hexObject.GetComponent<MeshCollider>();

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
    private void AddStartObjects()
    {
        // Add Crystals.
        Instantiate(crystal1Prefab, MapManager.Instance.Hexs[4, 0].transform);
        Instantiate(crystal2Prefab, MapManager.Instance.Hexs[4, 15].transform);

        // Add factories.
        var manager = FactoryManager.Instance;

        FactoryManager.Instance.CreateFactory(new Vector2Int(0, 14), Player.Player1, manager.factory1Level1);
        FactoryManager.Instance.CreateFactory(new Vector2Int(1, 14), Player.Player1, manager.factory1Level2);
        FactoryManager.Instance.CreateFactory(new Vector2Int(2, 14), Player.Player1, manager.factory1Level3);
        FactoryManager.Instance.CreateFactory(new Vector2Int(3, 14), Player.Player1, manager.factory2Level1);
        FactoryManager.Instance.CreateFactory(new Vector2Int(4, 14), Player.Player1, manager.factory2Level2);
        FactoryManager.Instance.CreateFactory(new Vector2Int(5, 14), Player.Player1, manager.factory2Level3);
        FactoryManager.Instance.CreateFactory(new Vector2Int(6, 14), Player.Player1, manager.factory3Level1);
        FactoryManager.Instance.CreateFactory(new Vector2Int(7, 14), Player.Player1, manager.factory3Level2Player1);
        FactoryManager.Instance.CreateFactory(new Vector2Int(8, 14), Player.Player1, manager.factory3Level3Player1);
        FactoryManager.Instance.CreateFactory(new Vector2Int(2, 15), Player.Player1, manager.factory4Level1);
        FactoryManager.Instance.CreateFactory(new Vector2Int(3, 15), Player.Player1, manager.factory4Level2);
        FactoryManager.Instance.CreateFactory(new Vector2Int(5, 15), Player.Player1, manager.factory4Level3);

        FactoryManager.Instance.CreateFactory(new Vector2Int(0, 1), Player.Player2, manager.factory1Level1);
        FactoryManager.Instance.CreateFactory(new Vector2Int(1, 1), Player.Player2, manager.factory1Level2);
        FactoryManager.Instance.CreateFactory(new Vector2Int(2, 1), Player.Player2, manager.factory1Level3);
        FactoryManager.Instance.CreateFactory(new Vector2Int(3, 1), Player.Player2, manager.factory2Level1);
        FactoryManager.Instance.CreateFactory(new Vector2Int(4, 1), Player.Player2, manager.factory2Level2);
        FactoryManager.Instance.CreateFactory(new Vector2Int(5, 1), Player.Player2, manager.factory2Level3);
        FactoryManager.Instance.CreateFactory(new Vector2Int(6, 1), Player.Player2, manager.factory3Level1);
        FactoryManager.Instance.CreateFactory(new Vector2Int(7, 1), Player.Player2, manager.factory3Level2Player2);
        FactoryManager.Instance.CreateFactory(new Vector2Int(8, 1), Player.Player2, manager.factory3Level3Player2);
        FactoryManager.Instance.CreateFactory(new Vector2Int(3, 0), Player.Player2, manager.factory4Level1);
        FactoryManager.Instance.CreateFactory(new Vector2Int(4, 0), Player.Player2, manager.factory4Level2);
        FactoryManager.Instance.CreateFactory(new Vector2Int(6, 0), Player.Player2, manager.factory4Level3);
    }
}
