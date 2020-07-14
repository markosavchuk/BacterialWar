using UnityEngine;
using System.Collections;

public class InfectionFactory : FactoryObject
{
    public override void Initialize()
    {
        base.Initialize();

        var mobProductionComponent = gameObject.AddComponent<InfectionFactoryMobProduction>();
        mobProductionComponent.ReproducableMob = MobCollection.Instance.InfectionMobLevel1;
    }

    public override void UpgradeFactory()
    {
        base.UpgradeFactory();

        FactoryBuilder.Instance.UpgrageFactoryPrefab(this);
    }

    protected override void SetFactoryCalculator()
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
}
