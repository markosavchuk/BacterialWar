using UnityEngine;
using System.Collections;

public class InfectionFactory : FactoryObject
{
    public override void Initialize()
    {
        base.Initialize();

        FactoryCalculator = gameObject.AddComponent<InfectionFactoryCalculator>();
        FactoryImage = FactoryImageCollection.Instance.InfectionFactoryAreaLevel1;

        var mobProductionComponent = gameObject.AddComponent<InfectionFactoryMobProduction>();
        mobProductionComponent.ReproducableMob = MobCollection.Instance.InfectionMobLevel1;
    }
}
