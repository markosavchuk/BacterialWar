using System;
public class BattleAreaFactoryParameters : FactoryParameters
{
    public float WaveDamage;

    public override (string Name, float Value) SpecialDamageValue
    {
        get => ("Damage", WaveDamage);
    }
}
