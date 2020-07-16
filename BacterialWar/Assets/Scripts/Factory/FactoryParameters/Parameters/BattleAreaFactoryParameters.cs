using System;
public class BattleAreaFactoryParameters : FactoryParameters
{
    public float WaveDamage = 5;

    public override (string Name, float Value) SpecialDamageValue
    {
        get => ("Damage", WaveDamage);
    }
}
