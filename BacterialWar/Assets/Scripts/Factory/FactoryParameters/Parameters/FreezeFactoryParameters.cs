using System;
using UnityEngine;
public class FreezeFactoryParameters : FactoryParameters
{
    public float Freeze;

    public override (string Name, float Value) SpecialDamageValue
    {
        get => ("Freeze", Freeze);
    }
}