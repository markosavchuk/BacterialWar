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

    public void Setup(FactoryObject factory)
    {
        _factoryImage.sprite = factory.FactoryImage;
        _factoryLevel.text = $"Level {factory.Level}";
    }
}
