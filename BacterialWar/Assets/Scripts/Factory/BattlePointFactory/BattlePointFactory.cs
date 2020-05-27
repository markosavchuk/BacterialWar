using UnityEngine;
using System.Collections;

public class BattlePointFactory : FactoryObject
{
    public override void Initialize()
    {
        base.Initialize();

        var mobProductionComponent = gameObject.AddComponent<BaseFactoryMobProduction>();
        mobProductionComponent.ReproducableMob = MobCollection.Instance.BattlePointMobLevel1;
    }
}
