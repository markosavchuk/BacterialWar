using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private List<HexObject> _factoriesHex;
    private int _primaryRow;
    private List<FactoryObject> _availableFactories;

    private void Awake()
    {
        _factoriesHex = MapManager.Instance.GetAllFactoryHexs(_player);
        _primaryRow = _player == Player.EnemyPlayer
            ? _factoriesHex.Max(h => h.MapPosition.y)
            : _factoriesHex.Min(h => h.MapPosition.y);

        _availableFactories = new List<FactoryObject>()
        {
            gameObject.AddComponent<BattleAreaFactory>(),
            gameObject.AddComponent<BattlePointFactory>(),
            gameObject.AddComponent<FreezeFactory>(),
            gameObject.AddComponent<InfectionFactory>()
        };

        foreach (var factory in _availableFactories)
        {
            factory.SetFactoryCalculator();
            factory.Parameters = factory.FactoryCalculator.GetParameters(1);
            factory.Player = _player;
        }
    }

    //todo refactor with change money event
    private void Update()
    {
        TryBuildOrUpgrade();
    }

    private void TryBuildOrUpgrade()
    {
        var availableMoney = MoneyManager.Instance.GetCurrentAmount(_player);

        // Build new factory if there is space for it
        var emptyPositions = _factoriesHex
            .Where(h => h.Content == null)
            .Select(h => h.MapPosition)
            .ToList();

        if (emptyPositions.Any())
        {
            var positionToBuild = ChoosePositionToBuildOrUpgrade(emptyPositions);
            if (positionToBuild.HasValue)
            {
                var factoryToBuild = ChooseNewFactory();
                if (availableMoney >= factoryToBuild.Parameters.Cost)
                {
                    FactoryBuilder.Instance.Build(factoryToBuild, positionToBuild.Value);
                }
            }
        }
        // Upgrage existing factory if no space for a new one
        else
        {
            var potensialFactories = ChooseFactoryToUpgarde();
            var potensialPositions = potensialFactories?
                .Select(f => f.MapPosition)
                .ToList();

            if (potensialFactories.Any())
            {
                var positionToUpgrade = ChoosePositionToBuildOrUpgrade(potensialPositions);
                if (positionToUpgrade.HasValue)
                {
                    var factoryToUpgrade = potensialFactories.FirstOrDefault(f => f.MapPosition.Equals(positionToUpgrade.Value));
                    if (availableMoney >= factoryToUpgrade.FactoryCalculator.GetParameters(factoryToUpgrade.Level + 1).Cost)
                    {
                        factoryToUpgrade.UpgradeFactory();
                    }
                }
            }
        }
    }
    
    private Vector2Int? ChoosePositionToBuildOrUpgrade(List<Vector2Int> positions)
    {
        if (positions==null || !positions.Any())
        {
            return null;
        }

        // Check positions for primary row
        var potensialPositions = positions
            .Where(p => p.y == _primaryRow)
            .ToList();

        // If no one on primary row then consider all other positions
        if (!potensialPositions.Any())
        {
            potensialPositions = positions;
        }

        //todo add some random thing here

        // Choose median within available positions
        var medianPosition = potensialPositions.ElementAt(potensialPositions.Count % 2 == 0
            ? potensialPositions.Count / 2
            : (potensialPositions.Count - 1) / 2);

        return medianPosition;
    }

    //todo improve it
    private FactoryObject ChooseNewFactory()
    {
        var cheapestFactory = _availableFactories
            .Aggregate((i1, i2) => i1.Parameters.Cost < i2.Parameters.Cost ? i1 : i2);
        return cheapestFactory;
    }

    //todo don't work for two farest factories
    private IEnumerable<FactoryObject> ChooseFactoryToUpgarde()
    {
        // Find all factories that could be upgraded
        var builtFactories = _factoriesHex
            .Where(h => h.Content != null && h.Content as FactoryObject)
            .Select(h => h.Content as FactoryObject);

        if (!builtFactories.Any())
        {
            return null;
        }

        // Order it by upgrade cost
        var ordered = builtFactories
            .OrderBy(f => f.FactoryCalculator.GetParameters(f.Level + 1).Cost)
            .ToList();

        // Choose the first third of it by cost
        int amountToConsider = ordered.Count / 3;
        if (amountToConsider < 1)
        {
            amountToConsider = 1;
        }

        return ordered.Take(amountToConsider);
    }
}
