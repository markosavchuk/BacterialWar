using System;
public class BattlePointFactoryParameters : FactoryParameters
{
    public float Damage = 50;

    public override (string Name, float Value) SpecialDamageValue
    {
        get => ("Damage", Damage);
    }
}
