using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystalinitializer : MonoBehaviour
{
    [SerializeField]
    private GameObject _crystal1Prefab;

    [SerializeField]
    private GameObject _crystal2Prefab;

    private void Start()
    {
        AddCrystals();
    }

    private void AddCrystals()
    {
        var mapCenter = MapManager.Instance.Width / 2;
        var offset = MapManager.Instance.Width % 2 == 0 ? -1 : 0;

        AddCrystal(_crystal1Prefab, new Vector2Int(mapCenter, 0), Player.Player1);
        AddCrystal(_crystal2Prefab, new Vector2Int(mapCenter + offset, MapManager.Instance.Height - 1), Player.Player2); ;
    }

    private void AddCrystal(GameObject crystalPrefab, Vector2Int position, Player player)
    {
        var parentHex = MapManager.Instance.Hexs[position.x, position.y];

        var crystal = Instantiate(crystalPrefab, parentHex.transform);
        var crystalObject = crystal.AddComponent<CrystalObject>();
        crystalObject.Player = player;

        parentHex.SetContent(crystalObject);
    }
}
