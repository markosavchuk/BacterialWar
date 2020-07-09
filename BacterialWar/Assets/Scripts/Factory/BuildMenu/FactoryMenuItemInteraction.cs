using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FactoryMenuItemInteraction : MonoBehaviour
{
    public void ChooseFactory(FactoryObject factoryObject)
    {
        FactoryBuilder.Instance.BuildMenu.GetComponent<BuildMenuInteraction>().FadeOutAllItems();

        var img = gameObject.GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
    }

    public void BuildFactory(FactoryObject factoryObject)
    {
        FactoryBuilder.Instance.Build(factoryObject);
        FactoryBuilder.Instance.BuildMenu.SetActive(false);
    }
}
