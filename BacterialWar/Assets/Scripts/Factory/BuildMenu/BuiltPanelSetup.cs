using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuiltPanelSetup : MonoBehaviour
{
    [SerializeField]
    private Image _factoryImage;

    [SerializeField]
    private Text _factoryLevel;

    [SerializeField]
    private Text _factoryHealh;

    private FactoryHexObject _factoryHexObject;

    private void OnDestroy()
    {
        _factoryHexObject.HealthUpdated -= UpdateData;
    }

    private void BaseSetup(FactoryHexObject factoryHexObject)
    {
        _factoryHexObject = factoryHexObject;

        _factoryHexObject.HealthUpdated -= UpdateData;
        _factoryHexObject.HealthUpdated += UpdateData;

        SetHealth();
    }

    public void Setup(FactoryObject factory)
    {
        BaseSetup(factory);

        _factoryImage.sprite = factory.FactoryImage;
        _factoryLevel.text = $"Level {factory.Level}";
    }

    public void Setup(CrystalObject crystal)
    {
        BaseSetup(crystal);

        _factoryImage.sprite = crystal.CrystalImage;
        _factoryLevel.text = "Source";
    }

    private void UpdateData(object sender, float health)
    {
        if (_factoryHealh != null)
        {
            SetHealth();
        }
    }

    private void SetHealth()
    {
        _factoryHealh.text = $"{Mathf.Round(_factoryHexObject.Health)}/{Mathf.Round(_factoryHexObject.InitialHealth)}";
    }
}
