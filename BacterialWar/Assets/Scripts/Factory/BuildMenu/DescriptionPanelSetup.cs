using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionPanelSetup : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Text _name;

    [SerializeField]
    private Text _health;

    [SerializeField]
    private Text _damage;

    [SerializeField]
    private Text _radius;

    [SerializeField]
    private Text _speed;

    [SerializeField]
    private Text _defense;

    [SerializeField]
    private Text _cost;

    public void Setup(FactoryObject factory)
    {
        FactoryParameters parameters = factory.FactoryCalculator.GetParameters(1);

        _image.sprite = parameters.MobImage;
        _name.text = parameters.Name;
        _health.text = $"Health: {parameters.Health}";
        _damage.text = $"Damage: {parameters.Damage}";
        _radius.text = $"Radius: {parameters.RiachRange}";
        _speed.text = $"Health: {parameters.Speed}";
        _cost.text = $"Plant ({parameters.Cost})";
    }
}
