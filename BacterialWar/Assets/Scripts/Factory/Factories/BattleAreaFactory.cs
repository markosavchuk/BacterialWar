﻿using UnityEngine;
using System.Collections;

public class BattleAreaFactory : FactoryObject
{
    public override void Initialize()
    {
        base.Initialize();

        var mobProductionComponent = gameObject.AddComponent<BattleAreaFactoryMobProduction>();
        mobProductionComponent.ReproducableMob = MobCollection.Instance.BattleAreaMobLevel1;
    }
}