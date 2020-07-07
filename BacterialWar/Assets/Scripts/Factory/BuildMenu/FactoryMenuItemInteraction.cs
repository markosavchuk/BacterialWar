using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FactoryMenuItemInteraction : MonoBehaviour
{
    public void BuildFactory(FactoryObject factoryObject)
    {
        FactoryBuilder.Instance.Build(factoryObject);
        FactoryBuilder.Instance.BuildMenu.SetActive(false);
    }
}
