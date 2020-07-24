using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystalinitializer : MonoBehaviour
{
    [SerializeField]
    private GameObject _crystal1Prefab;

    [SerializeField]
    private GameObject _crystal2Prefab;

    [SerializeField]
    private Sprite _crystal1Image;

    [SerializeField]
    private Sprite _crystal2Image;

    [SerializeField]
    private float _health;

    private void Start()
    {
        AddCrystals();
    }

    private void AddCrystals()
    {
        var mapCenter = MapManager.Instance.Width / 2;
        var offset = MapManager.Instance.Width % 2 == 0 ? -1 : 0;

        AddCrystal(_crystal1Prefab, new Vector2Int(mapCenter, 0), _crystal1Image);
        AddCrystal(_crystal2Prefab, new Vector2Int(mapCenter + offset, MapManager.Instance.Height - 1), _crystal2Image); ;
    }

    private void AddCrystal(GameObject crystalPrefab, Vector2Int position, Sprite crystalImage)
    {
        var parentHex = MapManager.Instance.Hexs[position.x, position.y];

        var crystal = Instantiate(crystalPrefab, parentHex.transform);
        var crystalObject = crystal.AddComponent<CrystalObject>();
        crystalObject.Player = parentHex.Player;
        crystalObject.Health = _health;
        crystalObject.StartHealth = _health;
        crystalObject.CrystalImage = crystalImage;

        parentHex.SetContent(crystalObject);
    }
}
