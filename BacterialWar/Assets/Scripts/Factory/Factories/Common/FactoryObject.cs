using System;
using UnityEngine;

public abstract class FactoryObject : FactoryHexObject
{
    public MobObject MobAbove { get; private set; }

    public BaseFactoryCalculator FactoryCalculator;

    public int Level = 1;

    public Sprite FactoryImage;

    public FactoryParameters Parameters;

    protected BaseFactoryMobProduction MobProduction;

    public void SetMobAbove(MobObject mob)
    { 
        if (mob == null)
        {
            MobAbove.OnFactory = null;
            MobAbove = null;
            return;
        }

        MobAbove = mob;

        mob.MapPosition = MapPosition;
        mob.OnFactory = this;
    }

    public virtual void Initialize()
    {
        SetFactoryCalculator();
        SetFactoryImage();

        Parameters = FactoryCalculator.GetParameters(1);

        InitializeMobProduction();

        Health = Parameters.FactoryHealth;
    }

    public virtual void UpgradeFactory()
    {
        Level++;

        SetFactoryImage();

        Parameters = FactoryCalculator.GetParameters(Level);

        MobProduction.OnUpgrade();

        SetReproducableMob();

        Health = Parameters.FactoryHealth;
    }

    protected virtual void InitializeMobProduction()
    {
        if (MobProduction == null)
        {
            return;
        }

        SetReproducableMob();
    }

    protected abstract void SetFactoryImage();

    protected abstract void SetFactoryCalculator();

    protected abstract void SetReproducableMob();
}
