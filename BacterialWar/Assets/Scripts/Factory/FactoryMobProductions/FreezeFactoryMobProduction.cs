using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeFactoryMobProduction : BaseFactoryMobProduction
{
    protected override void InitializeNewMob(GameObject mobInstance)
    {
        base.InitializeNewMob(mobInstance);

        var freezeAttacker = mobInstance.AddComponent<FreezeAttacker>();
        freezeAttacker.FightPeriod = 2;
    }
}
