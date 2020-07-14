using System;
using UnityEngine;

public abstract class FactoryObject : HexContent
{
    public MobObject MobAbove { get; private set; }

    public BaseFactoryCalculator FactoryCalculator;

    public int Level = 1;

    public Sprite FactoryImage;

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
    }

    public virtual void UpgradeFactory()
    {
        Level++;

        SetFactoryImage();

        //todo update parameters
    }

    protected abstract void SetFactoryImage();

    protected abstract void SetFactoryCalculator();
}
