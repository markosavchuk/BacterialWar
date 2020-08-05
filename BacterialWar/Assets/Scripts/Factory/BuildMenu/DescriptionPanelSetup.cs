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

    [SerializeField]
    private Button _button;

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
        _radius.text = FillParameterText("Radius", newParameters.RiachRange, oldParameters?.RiachRange);
        _speed.text = FillParameterText("Attack speed", newParameters.AttackSpeed, oldParameters?.AttackSpeed);
        _generationSpeed.text = FillParameterText("Generation", newParameters.GenetaionSpeed, oldParameters?.GenetaionSpeed);
        _defense.text = FillParameterText("Defense", newParameters.Defense, oldParameters?.Defense, "%");

        _cost.text = (isNewFactory ? $"Plant" : "Upgrade") + $" ({newParameters.Cost})";

        _damage.text = FillParameterText(newParameters.SpecialDamageValue.Name, newParameters.SpecialDamageValue.Value, oldParameters?.SpecialDamageValue.Value);
    }

    public void SetButtonInteraction(bool interactable)
    {
        _button.interactable = interactable;
    }

    private string FillParameterText(string name, float newParameter, float? oldParameter = null, string additionalSign = "")
    {
        var maxValuetoShowDecimals = 9;

        newParameter = newParameter > maxValuetoShowDecimals ? (int)newParameter : newParameter;
        if (oldParameter.HasValue)
        {
            oldParameter = oldParameter > maxValuetoShowDecimals ? (int)oldParameter : oldParameter;
        }

        var str = $"{name}: {string.Format("{0:0.#}", newParameter)}{additionalSign}";
        if (oldParameter.HasValue)
        {
            var deltaStr = newParameter > oldParameter ? $" (+{string.Format("{0:0.#}", newParameter - oldParameter)})" : string.Empty;
            str = str + deltaStr;
        }

        return str;
    }
}
