using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryNodeInteraction : MonoBehaviour
{
    HexObject _hexObject;

    private void Awake()
    {
        _hexObject = gameObject.GetComponent<HexObject>();
    }

    private void OnMouseDown()
    {
        var factoryBuilder = FactoryBuilder.Instance;

        if (factoryBuilder.SelectedFactoryPosition.HasValue)
        {
            return;
        }

        factoryBuilder.SelectedFactoryPosition = _hexObject.MapPosition;
        factoryBuilder.BuildMenu.SetActive(true);
    }
}
