using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private float _stepPeriod;

    private float _time = 0f;

    private List<HexObject> _factoriesHex;
    private int _primaryRow;
    private List<FactoryObject> _availableFactories;
    private List<(float Value, Type Type)> _planedFactoryCorrelations;

    #region Initialization
    private void Awake()
    {
        _factoriesHex = MapManager.Instance.GetAllFactoryHexs(_player);
        _primaryRow = _player == Player.EnemyPlayer
            ? _factoriesHex.Max(h => h.MapPosition.y)
            : _factoriesHex.Min(h => h.MapPosition.y);

        _availableFactories = GetAvailableFactories();
        _planedFactoryCorrelations = GetPlanedFactoryCorellations();
    }
    
    private List<FactoryObject> GetAvailableFactories()
    {
        var availableFactories = new List<FactoryObject>()
        {
            gameObject.AddComponent<BattleAreaFactory>(),
            gameObject.AddComponent<BattlePointFactory>(),
            gameObject.AddComponent<FreezeFactory>(),
            gameObject.AddComponent<InfectionFactory>()
        };

        foreach (var factory in availableFactories)
        {
            factory.SetFactoryCalculator();
            factory.Parameters = factory.FactoryCalculator.GetParameters(1);
            factory.Player = _player;
        }

        return availableFactories;
    }

    private List<(float Value, Type Type)> GetPlanedFactoryCorellations()
    {
        var corellations = new List<(float Value, Type Type)>
        {
            (2 + UnityEngine.Random.Range(0f, 1f), typeof(BattleAreaFactory)),
            (2 + UnityEngine.Random.Range(0f, 1f), typeof(BattlePointFactory)),
            (1 + UnityEngine.Random.Range(0f, 0.5f), typeof(FreezeFactory)),
            (1 + UnityEngine.Random.Range(0f, 0.5f), typeof(InfectionFactory))
        };

        return corellations;
    }
    #endregion

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _stepPeriod)
        {
            _time -= _stepPeriod;

            TryBuildOrUpgrade();
        }        
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

            if (potensialFactories!=null && potensialFactories.Any())
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

        // Choose median within available positions
        Vector2Int GetMedianPosition(List<Vector2Int> positionsToChoose)
        {
            return positionsToChoose.ElementAt(positionsToChoose.Count % 2 == 0
                ? positionsToChoose.Count / 2
                : (positionsToChoose.Count - 1) / 2);
        }

        // Randomly choose between top two positions
        var position1 = GetMedianPosition(potensialPositions);

        potensialPositions.Remove(position1);
        if (potensialPositions.Any())
        {
            var position2 = GetMedianPosition(potensialPositions);

            return UnityEngine.Random.Range(0, 2) == 0
                ? position1
                : position2;
        }
        else
        {
            return position1;
        }        
    }

    private List<(float Value, Type Type)> GetCurrentFactoryCorrelations()
    {
        var builtFactories = GetAllBuildFactories();

        return builtFactories
            .GroupBy(f => f.GetType())
            .Select(g => ((float)g.Count(), g.Key))
            .ToList();
    }

    private FactoryObject ChooseNewFactory()
    {
        // Calculate what factory should be build according to planned correlactions
        var currentCorrelation = GetCurrentFactoryCorrelations();
        var delta = new List<(float Value, Type Type)>();       

        foreach (var correlation in _planedFactoryCorrelations)
        {
            var currentValue = currentCorrelation
                .FirstOrDefault(c => c.Type == correlation.Type)
                .Value;

            delta.Add((currentValue / correlation.Value, correlation.Type));
        }

        var minValue = delta.Min(c => c.Value);
        var factoryWithMinCorrelations = delta.Where(c => c.Value == minValue).Select(c => c.Type);
        var factoryToChoose = _availableFactories.Where(f => factoryWithMinCorrelations.Contains(f.GetType()));

        // Choose the cheapest factory between available
        var cheapestFactory = factoryToChoose
            .Aggregate((i1, i2) => i1.Parameters.Cost < i2.Parameters.Cost ? i1 : i2);

        return cheapestFactory;
    }

    private IEnumerable<FactoryObject> ChooseFactoryToUpgarde()
    {
        // Find all factories that could be upgraded
        var builtFactories = GetAllBuildFactories();

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

    private IEnumerable<FactoryObject> GetAllBuildFactories()
    {
        return _factoriesHex
            .Where(h => h.Content != null && h.Content as FactoryObject)
            .Select(h => h.Content as FactoryObject);
    }
}
