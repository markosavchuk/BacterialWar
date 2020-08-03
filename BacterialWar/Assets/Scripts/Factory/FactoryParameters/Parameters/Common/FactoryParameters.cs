using System;
using UnityEngine;

public abstract class FactoryParameters
{
    public string Name;
    public int Level = 1;
    public float Health;
    public int RiachRange;
    public float AttackSpeed;
    public float Defense;
    public int Cost;
    public Sprite MobImage;
    public float GenetaionSpeed;
    public float FactoryHealth;
    public float RewardForDestroyingMob;

    public abstract (string Name, float Value) SpecialDamageValue { get; }
}
