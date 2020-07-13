using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryNodeInteraction : MonoBehaviour
{
    private HexObject _hexObject;

    private void Awake()
    {
        _hexObject = gameObject.GetComponent<HexObject>();
    }

    private void OnMouseDown()
    {
        if (_hexObject.Player != Player.Player1)
        {
            return;
        }

        FactoryBuilder.Instance.BuildMenu.OpenMenu(_hexObject);
    }    
}
