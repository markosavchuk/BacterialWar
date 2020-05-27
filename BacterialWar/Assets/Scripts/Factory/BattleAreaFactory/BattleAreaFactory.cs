using UnityEngine;
using System.Collections;

public class BattleAreaFactory : FactoryObject
{
    public override void Initialize()
    {
        base.Initialize();

        var mobProductionComponent = gameObject.AddComponent<BaseFactoryMobProduction>();
        mobProductionComponent.ReproducableMob = MobCollection.Instance.BattleAreaMobLevel1;
    }
}
