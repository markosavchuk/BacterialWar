using System;
using UnityEngine;
public class FreezeFactoryParameters : FactoryParameters
{
    public float Freeze = 1;

    public override (string Name, float Value) SpecialDamageValue
    {
        get => ("Freeze", Freeze);
    }
}