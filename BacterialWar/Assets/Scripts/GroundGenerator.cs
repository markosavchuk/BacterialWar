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
    private GameObject BattleHexPrefab;

    [SerializeField]
    private GameObject FactoryHexPrefab;

    [SerializeField]
    private int GridWidth;

    [SerializeField]
    private int GridHeight;

    [SerializeField]
    private int FactoryHeight;

    [SerializeField]
    private float Gap;  
 
    private void Start()
    {
        MapManager.Instance.Hexs = new GameObject[GridHeight, GridWidth];

        CalculateGap();
        CalculateStartPosition();
        CreateGrid();
    }

    private void CalculateGap()
    {
        _hexWidthWithGap = _hexWidth * (1+Gap);
        _hexHeightWithGap = _hexHeight * (1+Gap);
    }

    private void CalculateStartPosition()
    {
        var centerPosition = transform.position;

        float offsetX = centerPosition.x;

        if (GridHeight / 2 % 2 != 0)
        {
            offsetX += _hexWidthWithGap / 2;
        }

        float x = -_hexWidthWithGap * (GridWidth / 2) - offsetX;
        float z = _hexHeightWithGap * 0.75f * (GridHeight / 2) - centerPosition.z;

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
        for (int y = 0; y < GridHeight; y++)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                var hexType = y < FactoryHeight || y > GridHeight - FactoryHeight - 1
                    ? HexType.Factory
                    : HexType.Battle;

                var hexObject = hexType == HexType.Factory
                    ? Instantiate(FactoryHexPrefab)
                    : Instantiate(BattleHexPrefab);

                hexObject.transform.position = CalculateHexPosition(x, y);
                hexObject.transform.parent = this.transform;
                hexObject.name = $"Hexagon[{y},{x}]";

                var hexComponent = hexObject.AddComponent<HexComponent>() as HexComponent;
                hexComponent.HexType = hexType;


                MapManager.Instance.Hexs[y, x] = hexObject;
            }
        }
    }
}
