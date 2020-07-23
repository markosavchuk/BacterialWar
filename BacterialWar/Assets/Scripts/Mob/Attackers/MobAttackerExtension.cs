using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MobAttackerExtension
{
    public enum ChooseVictimStrategy
    {
        TheWeakest,
        TheStrongest
    }

    public static IEnumerable<Vector2Int> GetRichArea(MobObject mob)
    {
        var listOfPositions = new List<Vector2Int>();

        var range = mob.RiachRange;
        var position = mob.MapPosition;

        var maxRange = range * 2 + 1;
        for (int y = position.y - range; y <= position.y + range; y++)
        {
            if (y < 0 || y >= MapManager.Instance.Height)
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

            for (int x = startX; x <= endX; x++)
            {
                if (x < 0 || x >= MapManager.Instance.Width)
                {
                    continue;
                }

                listOfPositions.Add(new Vector2Int(x, y));
            }
        }

        return listOfPositions;
    }

    public static IEnumerable<HexContent> GetEnemyInArea(IEnumerable<Vector2Int> area, Player player, bool includeFactories)
    {
        // Extract content from area positions
        var areaContents = area
            .Select(p => MapManager.Instance.Hex(p).Content)
            .Where(c => c != null);

        // Find factories with mob above
        var factoriesWithMobAbove = areaContents
            .Where(c => c is FactoryObject factory && factory.MobAbove != null);

        // Extract Mobs from these factories
        var mobsOnFactories = factoriesWithMobAbove
            .Select(c => (c as FactoryObject).MobAbove);

        // Add mobs on factories instead of these factories
        areaContents = areaContents
            .Where(h => !factoriesWithMobAbove.Contains(h))
            .Union(mobsOnFactories);

        if (!includeFactories)
        {
            // Exclude all factories from list
            areaContents = areaContents.Where(c => c is MobObject);            
        }

        return areaContents.Where(m => m.Player != player);
    }

    public static HexContent ChooseVictim(IEnumerable<HexContent> enemies, ChooseVictimStrategy strategy)
    {
        if (enemies.Any())
        {
            // Choose the one who has the lower health.
            return enemies.Aggregate((m1, m2) => CompareVictims(m1, m2, strategy));
        }
        else
        {
            return null;
        }
    }

    public static HexContent CompareVictims(HexContent obj1, HexContent obj2, ChooseVictimStrategy strategy)
    {
        switch (strategy)
        {
            case ChooseVictimStrategy.TheStrongest:
                return obj1.Health > obj2.Health ? obj1 : obj2;
            case ChooseVictimStrategy.TheWeakest:
                return obj1.Health < obj2.Health ? obj1 : obj2;
            default:
                return obj1;
        }
    }
}
