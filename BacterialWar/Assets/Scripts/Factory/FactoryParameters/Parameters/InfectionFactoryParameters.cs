using System;
public class InfectionFactoryParameters : FactoryParameters
{
    public float Infection;

    public override (string Name, float Value) SpecialDamageValue
    {
        get => ("Infection", Infection);
    }
}
