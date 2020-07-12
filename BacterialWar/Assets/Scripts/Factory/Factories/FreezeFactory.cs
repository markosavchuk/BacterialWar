﻿using UnityEngine;
using System.Collections;

public class FreezeFactory : FactoryObject
{
    public override void Initialize()
    {
        base.Initialize();

        var mobProductionComponent = gameObject.AddComponent<FreezeFactoryMobProduction>();
        mobProductionComponent.ReproducableMob = MobCollection.Instance.FreezeMobLevel1;
    }
}