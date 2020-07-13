using System;
using UnityEngine;

public class FactoryObject : HexContent
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
    }    
}
