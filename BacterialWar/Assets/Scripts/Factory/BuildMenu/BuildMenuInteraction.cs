using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject _descriptionPanel;

    [SerializeField]
    private GameObject _newFactoryPanel;

    [SerializeField]
    private GameObject _builtFactoryPanel;

    private FactoryObject _activeFactoryDescription;
    private Vector2Int? _selectedFactoryPosition;
    private FactoryObject _factoryToUpgrage;

    public void OpenMenu(HexObject hexObject)
    {
        if (_selectedFactoryPosition.HasValue)
        {
            return;
        }

        _selectedFactoryPosition = hexObject.MapPosition;

        gameObject.SetActive(true);

        if (hexObject.Content == null)
        {
            _newFactoryPanel.SetActive(true);
        }
        else
        {
            if (hexObject.Content is FactoryObject factory)
            {
                _factoryToUpgrage = factory;

                _builtFactoryPanel.GetComponent<BuiltPanelSetup>().Setup(_factoryToUpgrage);
                _builtFactoryPanel.SetActive(true);

                _descriptionPanel.GetComponent<DescriptionPanelSetup>().Setup(_factoryToUpgrage, false);
                _descriptionPanel.SetActive(true);
            }
        }
    }

    public void CloseMenu()
    {
        _selectedFactoryPosition = null;
        _factoryToUpgrage = null;

        FadeOutAllItems(true);

        gameObject.SetActive(false);
        _newFactoryPanel.SetActive(false);
        _builtFactoryPanel.SetActive(false);
        _descriptionPanel.SetActive(false);

        _activeFactoryDescription = null;        
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

        _descriptionPanel.GetComponent<DescriptionPanelSetup>().Setup(factoryObject, true);

        _descriptionPanel.SetActive(true);
    }

    public void BuildFactory()
    {
        if (!_selectedFactoryPosition.HasValue)
        {
            return;
        }

        if (_factoryToUpgrage != null)
        {
            _factoryToUpgrage.UpgradeFactory();
        }
        else
        {
            FactoryBuilder.Instance.Build(_activeFactoryDescription, _selectedFactoryPosition.Value);
        }

        _selectedFactoryPosition = null;

        CloseMenu();
    }
}
