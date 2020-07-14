using UnityEngine;
using System.Collections;

public class BattlePointFactory : FactoryObject
{
    public override void Initialize()
    {
        base.Initialize();

        FactoryCalculator = gameObject.AddComponent<BattlePointFactoryCalculator>();
        FactoryImage = FactoryImageCollection.Instance.BattlePointFactoryLevel1;

        var mobProductionComponent = gameObject.AddComponent<BattlePointFactoryMobProduction>();
        mobProductionComponent.ReproducableMob = MobCollection.Instance.BattlePointMobLevel1;
    }

    public override void UpgradeFactory()
    {
        base.UpgradeFactory();

        FactoryBuilder.Instance.UpgrageFactoryPrefab(this);
    }
}
