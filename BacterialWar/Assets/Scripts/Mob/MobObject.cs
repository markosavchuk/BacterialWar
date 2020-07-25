using UnityEngine;
using System.Collections;
using System;

public class MobObject : HexContent
{
    /// <summary>
    /// Automatically assigned on FactoryObject class
    /// </summary>
    public FactoryObject OnFactory;

    public int RiachRange;

    public float FrozenFight = 0;
    public float FrozenMovement = 0;
    public bool IsInMotion = false;
    public float Infection = 0;

    private float _time = 0f;
    private StateParticle _frozenParticles;
    private StateParticle _infectionParticles;

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

    public override void GotAttacked(float damage, float freezeMovementTime)
    {
        base.GotAttacked(damage, freezeMovementTime);

        FreezeMovement(freezeMovementTime);
    }

    public void GotInfected(float permanentDamage)
    {
        Infection += permanentDamage;

        if (_infectionParticles == null)
        {
            _infectionParticles = AddStateParticles(ParticleCollection.Instance.Infected);
        }
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

            if (_frozenParticles == null)
            {
                _frozenParticles = AddStateParticles(ParticleCollection.Instance.Frozen, freezeTime);
            }
            else
            {
                _frozenParticles.Lifetime = freezeTime;
            }
        }
    }

    private void DamageInfection()
    {
        DamageObject(Infection);
    }

    private StateParticle AddStateParticles(GameObject particlesPrefab, float? time = null)
    {
        var particles = Instantiate(particlesPrefab, gameObject.transform);
        particles.transform.position = gameObject.transform.position + new Vector3(0, 0.5f, 0);

        var stateParticle = particles.AddComponent<StateParticle>();
        stateParticle.Lifetime = time;

        return stateParticle;
    }

    protected override void DestroyObject()
    {
        if (_frozenParticles != null)
        {
            _frozenParticles.Lifetime = 0;
        }

        if (_infectionParticles != null)
        {
            _infectionParticles.Lifetime = 0;
        }

        if (OnFactory == null)
        {
            ParentHex.BookedForPlayer = Player == Player.MyPlayer
                ? Player.EnemyPlayer
                : Player.MyPlayer;
        }
        else
        {
            OnFactory.SetMobAbove(null);
        }

        base.DestroyObject();
    }
}
