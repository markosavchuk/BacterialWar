using System;
public class BattlePointFactoryParameters : FactoryParameters
{
    public float Damage;

    public override (string Name, float Value) SpecialDamageValue
    {
        get => ("Damage", Damage);
    }
}
