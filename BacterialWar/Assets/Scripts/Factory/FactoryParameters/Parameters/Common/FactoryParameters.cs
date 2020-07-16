using System;
using UnityEngine;

public abstract class FactoryParameters
{
    public const float GenerationSpeedConst = 2;
    public const float AttackSpeedConst = 1;

    //todo apply this parameters into mob objects and other scripts
    public string Name;
    public float Health = 120;
    public int RiachRange = 2;
    public float Speed = 2;
    public float Defense = 3;
    public int Cost = 200;
    public Sprite MobImage;
    public int GenetaionSpeed = 1;
    public int Level = 1;

    public abstract (string Name, float Value) SpecialDamageValue { get; }
}
