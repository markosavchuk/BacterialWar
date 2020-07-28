using UnityEngine;
using System.Collections;

public class InfectionFactory : FactoryObject
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
        MobProduction = gameObject.AddComponent<InfectionFactoryMobProduction>();

        base.InitializeMobProduction();
    }

    public override void SetFactoryCalculator()
    {
        FactoryCalculator = gameObject.AddComponent<InfectionFactoryCalculator>();
    }

    protected override void SetFactoryImage()
    {
        var images = FactoryImageCollection.Instance;

        switch (Level)
        {
            case 1:
                FactoryImage = images.InfectionFactoryLevel1;
                break;
            case 2:
                FactoryImage = images.InfectionFactoryLevel2;
                break;
            case 3:
            default:
                FactoryImage = images.InfectionFactoryLevel3;
                break;
        }
    }

    protected override void SetReproducableMob()
    {
        switch (Level)
        {
            case 1:
                MobProduction.ReproducableMob = MobCollection.Instance.InfectionMobLevel1;
                break;
            case 2:
            default:
                MobProduction.ReproducableMob = MobCollection.Instance.InfectionMobLevel2;
                break;
        }
    }
}
