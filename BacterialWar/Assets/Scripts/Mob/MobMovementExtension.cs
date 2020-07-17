using UnityEngine;

public class MobMovementExtension
{
    public static Vector2Int? FindNewPlaceForMob(Vector2Int oldPlace, Player player)
    {
        var delta = oldPlace.y % 2 == 0 ? -1 : 1;
        var direction = player == Player.Player1 ? 1 : -1;

        var path1 = new Vector2Int(oldPlace.x, oldPlace.y - 1 * direction);
        var path2 = new Vector2Int(oldPlace.x + delta, oldPlace.y - 1 * direction);

        if (IsAvailable(path1, player) && IsAvailable(path2, player))
        {
            var randomPath = UnityEngine.Random.Range(0, 2);
            return randomPath == 0 ? path1 : path2;
        }
        else if (IsAvailable(path1, player))
        {
            return path1;
        }
        else if (IsAvailable(path2, player))
        {
            return path2;
        }
        else
        {
            return null;
        }
    }

    public static bool IsAvailable(Vector2Int position, Player player)
    {
        // Check if Map has this Hex position
        if (!MapManager.Instance.IsExist(position))
        {
            return false;
        }

        // Get hexComponent
        if (!(MapManager.Instance.Hex(position).GetComponent<HexObject>() is HexObject hexComponent))
        {
            return false;
        }

        // Check if this position allowed for Mob
        if (hexComponent.HexType != HexType.Battle)
        {
            // Check if it's enemy factory hex
            if (hexComponent.Player != player)
            {
                return false;
            }

            // Check if position is empty (for factory hex with factory)
            if (hexComponent.Content != null && hexComponent.Content is FactoryObject factory && factory?.MobAbove != null)
            {
                return false;
            }

            // Check if position is empty (for factory hex without factory)
            if (hexComponent.Content != null && hexComponent.Content is MobObject mobObject)
            {
                return false;
            }
        }
        // Check if position is empty (for mob hex)
        else if (hexComponent.Content != null)
        {
            return false;
        }

        // Check if position is booked
        if (hexComponent.BookedForPlayer.HasValue && hexComponent.BookedForPlayer != player)
        {
            return false;
        }

        // Check if game state allow it
        if (StateManager.Instance.GameState == GameState.Preparation)
        {
            if ((player == Player.Player1 && position.y < MapManager.Instance.Height / 2) ||
                (player == Player.Player2 && position.y >= MapManager.Instance.Height / 2))
            {
                return false;
            }
        }

        return true;
    }
}
