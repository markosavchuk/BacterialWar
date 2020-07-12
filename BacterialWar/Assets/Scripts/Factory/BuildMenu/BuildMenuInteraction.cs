using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject _descriptionPanel;

    private FactoryObject _activeFactoryDescription;

    public void CloseMenu()
    {       
        FactoryBuilder.Instance.SelectedFactoryPosition = null;
        gameObject.SetActive(false);

        _descriptionPanel.SetActive(false);
        _activeFactoryDescription = null;

        FadeOutAllItems(true);
    }

    public void FadeOutAllItems(bool oppositeAction = false)
    {
        var alfa = oppositeAction ? 1 : 0.35f;

        foreach (var item in gameObject.GetComponentsInChildren<FactoryMenuItemInteraction>(false))
        {
            var img = item.gameObject.GetComponent<Image>();
            img.color = new Color(img.color.r, img.color.g, img.color.b, alfa);
        }
    }

    public void OpenFactoryDescription(FactoryObject factoryObject)
    {
        _activeFactoryDescription = factoryObject;

        _descriptionPanel.GetComponent<DescriptionPanelSetup>().Setup(factoryObject);

        _descriptionPanel.SetActive(true);
    }

    public void BuildFactory()
    {
        FactoryBuilder.Instance.Build(_activeFactoryDescription);
        CloseMenu();
    }
}
