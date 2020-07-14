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

    public override void UpgradeFactory()
    {
        base.UpgradeFactory();

        FactoryBuilder.Instance.UpgrageFactoryPrefab(this);
    }

    protected override void SetFactoryCalculator()
    {
        FactoryCalculator = gameObject.AddComponent<BattlePointFactoryCalculator>();
    }

    protected override void SetFactoryImage()
    {
        var images = FactoryImageCollection.Instance;

        switch (Level)
        {
            case 1:
                FactoryImage = images.BattlePointFactoryLevel1;
                break;
            case 2:
                FactoryImage = images.BattlePointFactoryLevel2;
                break;
            case 3:
            default:
                FactoryImage = images.BattlePointFactoryLevel3;
                break;
        }
    }
}
