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
    private Text _generationSpeed;

    [SerializeField]
    private Text _cost;    

    public void Setup(FactoryObject factory, bool isNewFactory)
    {
        var calculator = factory.FactoryCalculator;
        var level = isNewFactory ? 1 : factory.Level + 1;

        FactoryParameters oldParameters = isNewFactory
            ? null
            : calculator.GetParameters(factory.Level);

        FactoryParameters newParameters = calculator.GetParameters(level);

        _image.sprite = newParameters.MobImage;
        _name.text = $"{newParameters.Name} (Lvl {level})";

        _health.text = FillParameterText("Health", newParameters.Health, oldParameters?.Health);
        _damage.text = FillParameterText("Damage", newParameters.Damage, oldParameters?.Damage);
        _radius.text = FillParameterText("Radius", newParameters.RiachRange, oldParameters?.RiachRange);
        _speed.text = FillParameterText("Speed", newParameters.Speed, oldParameters?.Speed);
        _generationSpeed.text = FillParameterText("Generation", newParameters.GenetaionSpeed, oldParameters?.GenetaionSpeed);

        _cost.text = isNewFactory ? "Plant" : "Upgrade" + $" ({newParameters.Cost})";
    }

    private string FillParameterText(string name, float newParameter, float? oldParameter = null)
    {
        newParameter = (int)newParameter;
        if (oldParameter.HasValue)
        {
            oldParameter = (int)oldParameter;
        }

        var str = $"{name}: {newParameter}";
        if (oldParameter.HasValue)
        {
            var deltaStr = newParameter > oldParameter ? $" (+{newParameter-oldParameter})" : string.Empty;
            str = str + deltaStr;
        }
        return str;
    }
}
