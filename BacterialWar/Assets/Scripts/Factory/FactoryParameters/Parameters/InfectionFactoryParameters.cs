using System;
public class InfectionFactoryParameters : FactoryParameters
{
    public float Infection = 5;

    public override (string Name, float Value) SpecialDamageValue
    {
        get => ("Infection", Infection);
    }
}
