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
}
