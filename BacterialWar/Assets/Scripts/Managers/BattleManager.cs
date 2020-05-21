﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

//todo split a bit or make regions at least
public class BattleManager : SingletonMonoBehaviour<BattleManager>
{
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

        if (_time >= Settings.StepTime)
        {
            _time -= Settings.StepTime;

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

            mapObjComponent.CanMove = true;

            if (mapObjComponent.IsInMotion)
            {
                continue;
            }

            var richArea = GetRichArea(mobObject);
            var richMobsObjects = richArea
                .Where(p => p != null)
                .Select(p => mobMatrix[p.x, p.y])
                .Where(o => o != null);

            var victimMob = ChooseVictim(richMobsObjects, mapObjComponent.Player);

            if (victimMob != null)
            {
                var isAlive = mobComponent.Attacked(victimMob.GetComponent<MobComponent>());
                if (!isAlive)
                {
                    RemoveMobFromMap(mobMatrix, victimMob);
                }

                mapObjComponent.CanMove = false;
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

    private GameObject ChooseVictim(IEnumerable<GameObject> mobs, Player player)
    {
        var enemies = mobs
            .Where(m => m.GetComponent<MapObjectComponent>().Player != player);

        if (enemies.Any())
        {
            // Choose the one who has the lower health.
            return enemies.Aggregate((m1, m2) =>
                m1.GetComponent<MobComponent>().Health <
                m2.GetComponent<MobComponent>().Health ? m1 : m2);
        }
        else
        {
            return null;
        }
    }

    //todo move it somewhere and make it generic for any map object
    private void RemoveMobFromMap(GameObject[,] mobMatrix, GameObject gameObject)
    {
        var position = gameObject.GetComponent<MapObjectComponent>().MapPosition;

        MapManager.Instance.Hex(position)
            .GetComponent<HexComponent>().ObjectAbove = null;

        mobMatrix[position.x, position.y] = null;

        Destroy(gameObject);
    }
}
