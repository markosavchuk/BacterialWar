using UnityEngine;
using System.Collections;

public class BattlePointFactory : FactoryObject
{
    public override void Initialize()
    {
        base.Initialize();

        var mobProductionComponent = gameObject.AddComponent<BattlePointFactoryMobProduction>();
        mobProductionComponent.ReproducableMob = MobCollection.Instance.BattlePointMobLevel1;
    }
}
