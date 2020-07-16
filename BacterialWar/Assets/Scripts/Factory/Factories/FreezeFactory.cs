using UnityEngine;
using System.Collections;

public class FreezeFactory : FactoryObject
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
        MobProduction = gameObject.AddComponent<FreezeFactoryMobProduction>();

        base.InitializeMobProduction();
    }

    protected override void SetFactoryCalculator()
    {
        FactoryCalculator = gameObject.AddComponent<FreezeFactoryCalculator>();
    }

    protected override void SetFactoryImage()
    {
        var images = FactoryImageCollection.Instance;

        switch (Level)
        {
            case 1:
                FactoryImage = images.FreezeFactoryLevel1;
                break;
            case 2:
                FactoryImage = images.FreezeFactoryLevel2;
                break;
            case 3:
            default:
                FactoryImage = images.FreezeFactoryLevel3;
                break;
        }
    }

    protected override void SetReproducableMob()
    {
        switch (Level)
        {
            case 1:
                MobProduction.ReproducableMob = MobCollection.Instance.FreezeMobLevel1;
                break;
            case 2:
            default:
                MobProduction.ReproducableMob = MobCollection.Instance.FreezeMobLevel2;
                break;
        }
    }
}
