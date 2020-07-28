using UnityEngine;
using System.Collections;

public class BattlePointFactory : FactoryObject
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public override void UpgradeFactory()
    {
        base.UpgradeFactory();

        FactoryBuilder.Instance.UpgrageFactoryPrefab(this);
    }

    protected override void InitializeMobProduction()
    {
        MobProduction = gameObject.AddComponent<BattlePointFactoryMobProduction>();

        base.InitializeMobProduction();
    }

    public override void SetFactoryCalculator()
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

    protected override void SetReproducableMob()
    {
        switch (Level)
        {
            case 1:
                MobProduction.ReproducableMob = MobCollection.Instance.BattlePointMobLevel1;
                break;
            case 2:
                MobProduction.ReproducableMob = MobCollection.Instance.BattlePointMobLevel2;
                break;
            case 3:
            default:
                MobProduction.ReproducableMob = MobCollection.Instance.BattlePointMobLevel3;
                break;
        }
    }
}
