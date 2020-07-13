using UnityEngine;
using System.Collections;

public class BattleAreaFactory : FactoryObject
{
    public override void Initialize()
    {
        base.Initialize();

        FactoryCalculator = gameObject.AddComponent<BattleAreaFactoryCalculator>();
        FactoryImage = FactoryImageCollection.Instance.BattleAreaFactoryLevel1;

        var mobProductionComponent = gameObject.AddComponent<BattleAreaFactoryMobProduction>();
        mobProductionComponent.ReproducableMob = MobCollection.Instance.BattleAreaMobLevel1;
    }
}
