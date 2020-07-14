using UnityEngine;
using System.Collections;

public class FreezeFactory : FactoryObject
{
    public override void Initialize()
    {
        base.Initialize();

        FactoryCalculator = gameObject.AddComponent<FreezeFactoryCalculator>();
        FactoryImage = FactoryImageCollection.Instance.FreezeFactoryLevel1;

        var mobProductionComponent = gameObject.AddComponent<FreezeFactoryMobProduction>();
        mobProductionComponent.ReproducableMob = MobCollection.Instance.FreezeMobLevel1;
    }

    public override void UpgradeFactory()
    {
        base.UpgradeFactory();

        FactoryBuilder.Instance.UpgrageFactoryPrefab(this);
    }
}
