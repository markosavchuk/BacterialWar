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

        Instantiate(_crystal1Prefab, MapManager.Instance.Hexs[mapCenter, 0].transform);
        Instantiate(_crystal2Prefab, MapManager.Instance.Hexs[mapCenter, MapManager.Instance.Height-1].transform);
    }
}
