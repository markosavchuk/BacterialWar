using System;
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

        if (hexObject.Content is MobObject)
        {
            return;
        }

        if (hexObject.Content == null && hexObject.Player != Player.MyPlayer)
        {
            return;
        }

        if (hexObject.Content == null && hexObject.Player == Player.MyPlayer &&
            StateManager.Instance.GameState == GameState.Lost)
        {
            return;
        }

        _selectedFactoryPosition = hexObject.MapPosition;
        gameObject.SetActive(true);

        if (hexObject.Content == null && hexObject.Player == Player.MyPlayer)
        {
            _newFactoryPanel.SetActive(true);
        }
        else if (hexObject.Content is FactoryHexObject factoryHexObject)
        {
            if (factoryHexObject is FactoryObject factory)
            {
                _factoryToUpgrage = factory;

                _builtFactoryPanel.GetComponent<BuiltPanelSetup>().Setup(_factoryToUpgrage);

                if (hexObject.Player == Player.MyPlayer)
                {
                    _descriptionPanel.GetComponent<DescriptionPanelSetup>().Setup(_factoryToUpgrage, false);
                    SetBuildOrUpgradeButtonInteraction(_factoryToUpgrage);

                    _descriptionPanel.SetActive(true);

                    MoneyManager.Instance.MyWalletUpdated -= OnMyWalletUpdated;
                    MoneyManager.Instance.MyWalletUpdated += OnMyWalletUpdated;
                }
            }
            else if (factoryHexObject is CrystalObject crystal)
            {
                _builtFactoryPanel.GetComponent<BuiltPanelSetup>().Setup(crystal);
            }

            _builtFactoryPanel.SetActive(true);

            factoryHexObject.HealthUpdated -= OnSelectedFactoryHealthUpdated;
            factoryHexObject.HealthUpdated += OnSelectedFactoryHealthUpdated;
        }
    }

    public void CloseMenu()
    {
        if (_factoryToUpgrage != null)
        {
            _factoryToUpgrage.HealthUpdated -= OnSelectedFactoryHealthUpdated;
        }

        MoneyManager.Instance.MyWalletUpdated -= OnMyWalletUpdated;

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

        SetBuildOrUpgradeButtonInteraction(factoryObject);

        _descriptionPanel.SetActive(true);

        MoneyManager.Instance.MyWalletUpdated -= OnMyWalletUpdated;
        MoneyManager.Instance.MyWalletUpdated += OnMyWalletUpdated;
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
            try
            {
                FactoryBuilder.Instance.Build(_activeFactoryDescription, _selectedFactoryPosition.Value);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }

        _selectedFactoryPosition = null;

        CloseMenu();
    }

    private void OnSelectedFactoryHealthUpdated(object sender, float health)
    {
        if (health <= 0)
        {
            CloseMenu();
        }
    }

    private void OnMyWalletUpdated(object sender, float value)
    {
        var factory = _activeFactoryDescription != null
            ? _activeFactoryDescription : _factoryToUpgrage;

        if (factory != null)
        {
            SetBuildOrUpgradeButtonInteraction(factory);
        }
    }

    private void SetBuildOrUpgradeButtonInteraction(FactoryObject factory)
    {
        bool canInteract;
        if (_factoryToUpgrage == null)
        {
            canInteract = MoneyManager.Instance.CanBuildOrUpgradeFactory(factory, 1);
        }
        else
        {
            canInteract = MoneyManager.Instance.CanBuildOrUpgradeFactory(factory, factory.Level + 1);
        }

        _descriptionPanel.GetComponent<DescriptionPanelSetup>()
            .SetButtonInteraction(canInteract);
    }
}
