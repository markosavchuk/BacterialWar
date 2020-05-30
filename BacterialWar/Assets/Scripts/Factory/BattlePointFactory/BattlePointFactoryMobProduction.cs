using UnityEngine;
using System.Collections;

public class BattlePointFactoryMobProduction : BaseFactoryMobProduction
{
    protected override void InitializeNewMob(GameObject mobInstance)
    {
        base.InitializeNewMob(mobInstance);

        mobInstance.AddComponent<BattlePointAttacker>();
    }
}
