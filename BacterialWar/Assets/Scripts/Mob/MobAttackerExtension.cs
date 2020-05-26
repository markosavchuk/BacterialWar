using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MobAttackerExtension
{
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

    public static IEnumerable<MobObject> GetMobsOnArea(IEnumerable<Vector2Int> area)
    {
        return area.Where(p => MapManager.Instance.Hex(p).Сontent is MobObject)
            .Select(p=> MapManager.Instance.Hex(p).Сontent as MobObject);
    }

    public static MobObject ChooseVictim(IEnumerable<MobObject> mobs, Player player)
    {
        var enemies = mobs.Where(m => m.Player != player);

        if (enemies.Any())
        {
            // Choose the one who has the lower health.
            return enemies.Aggregate((m1, m2) =>
                m1.Health < m2.Health ? m1 : m2);
        }
        else
        {
            return null;
        }
    }
}
