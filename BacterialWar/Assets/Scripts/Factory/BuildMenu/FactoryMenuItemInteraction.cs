using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FactoryMenuItemInteraction : MonoBehaviour
{
    private BuildMenuInteraction _buildMenuInteraction;

    private void Awake()
    {
        _buildMenuInteraction = FactoryBuilder.Instance.BuildMenu.GetComponent<BuildMenuInteraction>();
    }

    public void ChooseFactory(FactoryObject factoryObject)
    {
        //todo maybe refactor to call just one method
        _buildMenuInteraction.FadeOutAllItems();

        var img = gameObject.GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1);

        _buildMenuInteraction.OpenFactoryDescription(factoryObject);
    }

    public void BuildFactory()
    {
        //FactoryBuilder.Instance.Build(factoryObject);
        //FactoryBuilder.Instance.BuildMenu.SetActive(false);
    }
}
