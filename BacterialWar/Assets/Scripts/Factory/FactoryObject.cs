using System;
using UnityEngine;

public class FactoryObject : HexContent
{    
    public MobObject MobAbove { get; private set; }

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
        var mobProductionComponent = gameObject.AddComponent<BaseFactoryMobProduction>();
        mobProductionComponent.ReproducableMob = Player == Player.Player1
            ? MobCollection.Instance.Virus1
            : MobCollection.Instance.Virus2;
    }    
}
