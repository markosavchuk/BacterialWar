using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //todo make helper class for it
    #region Singleton logic    

    public static BattleManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    //todo move this value to another place;
    [SerializeField]
    private float battlePeriod;

    private float _time = 0f;

    private int _mapLengthX;
    private int _mapLengthY;

    private void Start()
    {
        _mapLengthX = MapManager.Instance.Hexs.GetLength(0);
        _mapLengthY = MapManager.Instance.Hexs.GetLength(1);
    }

    private void Update()
    {
        //todo find better way to track it
        _time += Time.deltaTime;

        if (_time >= battlePeriod)
        {
            _time -= battlePeriod;

            ExecuteRound();
        }
    }

    private void ExecuteRound()
    {
        if (StateManager.Instance.GameState== GameState.Preparation)
        {
            return;
        }

        var mobObjects = FindAllMobs();
        var mobMatrix = MakeMobMatrix(mobObjects);

        foreach (var mobObject in mobObjects)
        {
            var mobComponent = mobObject.GetComponent<MobComponent>();
            var mapObjComponent = mobObject.GetComponent<MapObjectComponent>();

            if (mapObjComponent.IsInMotion)
            {
                continue;
            }

            foreach (var position in GetRichArea(mobObject))
            {
                if (mobMatrix[position.x, position.y] != null)
                {
                    var enemyMapObjComponent = mobMatrix[position.x, position.y].GetComponent<MapObjectComponent>();
                    var enemyMobComponent = mobMatrix[position.x, position.y].GetComponent<MobComponent>();

                    if (enemyMapObjComponent.Player != mapObjComponent.Player)
                    {
                        var isAliva = enemyMobComponent.Attack(mobComponent);
                        if (!isAliva)
                        {
                            RemoveMobFromMap(mobMatrix, mobMatrix[position.x, position.y]);
                        }
                    }
                }
            }
        }
    }

    private List<GameObject> FindAllMobs()
    {
        return GameObject.FindGameObjectsWithTag(TagConstants.Mob).ToList();
    }

    private GameObject[,] MakeMobMatrix(List<GameObject> mobObjects)
    {
        
        var matrix = new GameObject[_mapLengthX, _mapLengthY];

        foreach (var mobObject in mobObjects)
        {
            var position = mobObject.GetComponent<MapObjectComponent>().MapPosition;
            matrix[position.x, position.y] = mobObject;
        }

        return matrix;
    }

    private List<Vector2Int> GetRichArea(GameObject gameObject)
    {
        var listOfPositions = new List<Vector2Int>();

        var range = gameObject.GetComponent<MobComponent>().RiachRange;
        var position = gameObject.GetComponent<MapObjectComponent>().MapPosition;

        var maxRange = range * 2 + 1;
        for (int y = position.y - range; y <= position.y + range; y++)
        {
            if (y < 0 || y >= _mapLengthY) 
            {
                continue;
            }

            var xRange = maxRange - Mathf.Abs(y - position.y);

            int startX;
            int endX;

            if (xRange % 2 != 0)
            {
                startX = position.x - (xRange - 1) / 2;
                endX = position.x + (xRange - 1) / 2;
            }
            else
            {
                startX = position.x - (xRange / 2 - 1);
                endX = position.x + (xRange / 2 - 1);

                if (y % 2 == 0)
                {
                    endX += 1;
                }
                else
                {
                    startX -= 1;
                }
            }

            for (int x=startX; x <= endX; x++)
            {
                if (x < 0 || x >= _mapLengthX)
                {
                    continue;
                }

                listOfPositions.Add(new Vector2Int(x, y));
            }
        }

        return listOfPositions;
    }

    private void RemoveMobFromMap(GameObject[,] mobMatrix, GameObject gameObject)
    {
        var position = gameObject.GetComponent<MapObjectComponent>().MapPosition;

        MapManager.Instance.Hex(position)
            .GetComponent<HexComponent>().ObjectAbove = null;

        mobMatrix[position.x, position.y] = null;

        Destroy(gameObject);
    }
}
