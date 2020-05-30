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

    public static IEnumerable<MobObject> GetEnemyMobsInArea(IEnumerable<Vector2Int> area, Player player)
    {
        return area.Where(p => MapManager.Instance.Hex(p).Сontent is MobObject)
            .Select(p=> MapManager.Instance.Hex(p).Сontent as MobObject)
            .Where(m => m.Player != player);
    }

    public static MobObject ChooseVictim(IEnumerable<MobObject> enemies, ChooseVictimStrategy strategy)
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

    public static MobObject CompareVictims(MobObject mob1, MobObject mob2, ChooseVictimStrategy strategy)
    {
        switch (strategy)
        {
            case ChooseVictimStrategy.TheStrongest:
                return mob1.Health > mob2.Health ? mob1 : mob2;
            case ChooseVictimStrategy.TheWeakest:
                return mob1.Health < mob2.Health ? mob1 : mob2;
            default:
                return mob1;
        }
    }
}
