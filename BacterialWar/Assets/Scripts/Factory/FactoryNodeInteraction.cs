using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryNodeInteraction : MonoBehaviour
{
    private HexObject _hexObject;
    private FactoryBuilder _factoryBuilder;

    private void Awake()
    {
        _hexObject = gameObject.GetComponent<HexObject>();
        _factoryBuilder = FactoryBuilder.Instance;
    }

    private void OnMouseDown()
    {
        if (!CanBuildFactory())
        {
            return;
        }

        _factoryBuilder.SelectedFactoryPosition = _hexObject.MapPosition;
        _factoryBuilder.BuildMenu.SetActive(true);
    }

    private bool CanBuildFactory()
    {
        if (_factoryBuilder.SelectedFactoryPosition.HasValue)
        {
            return false;
        }

        if (_hexObject.Player != Player.Player1)
        {
            return false;
        }

        if (_hexObject.Сontent != null)
        {
            return false;
        }

        return true;
    }
}
