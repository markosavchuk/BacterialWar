using UnityEngine;
using System.Collections;
using System;

public class MobObject : HexContent
{
    /// <summary>
    /// Automatically assigned on FactoryObject class
    /// </summary>
    public FactoryObject OnFactory;

    public float Health = 100;
    public float FrozenFight = 0;
    public float FrozenMovement = 0;
    public bool IsInMotion = false;
    public int RiachRange = 2;
    public float Infection = 0;

    private float _time = 0f;

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= Settings.Instance.StepTime)
        {
            _time -= Settings.Instance.StepTime;

            Unfreeze();
            DamageInfection();
        }
    }    

    public void GotAttacked(float damage, float freezeMovementTime)
    {
        FreezeMovement(freezeMovementTime);

        Health -= damage;
        if (Health <= 0)
        {
            DestroyObject();
        }
    }

    public void GotInfected(float permanentDamage)
    {
        Infection += permanentDamage;
    }

    private void Unfreeze()
    {
        if (FrozenFight > 0)
        {
            FrozenFight = FrozenFight < Settings.Instance.StepTime
                ? 0
                : FrozenFight - Time.deltaTime;
        }

        if (FrozenMovement > 0)
        {
            FrozenMovement = FrozenMovement < Settings.Instance.StepTime
                ? 0
                : FrozenMovement - Time.deltaTime;
        }
    }

    public void FreezeMovement(float freezeTime)
    {
        if (FrozenMovement < freezeTime)
        {
            FrozenMovement = freezeTime;
        }
    }

    public void FreezeFight(float freezeTime)
    {
        if (FrozenFight < freezeTime)
        {
            FrozenFight = freezeTime;
        }
    }

    private void DamageInfection()
    {
        Health -= Infection;
        if (Health <= 0)
        {
            DestroyObject();
        }
    }
}
