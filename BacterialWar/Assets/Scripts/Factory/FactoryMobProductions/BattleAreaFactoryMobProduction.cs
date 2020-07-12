using UnityEngine;
using System.Collections;

public class BattleAreaFactoryMobProduction : BaseFactoryMobProduction
{
    protected override void InitializeNewMob(GameObject mobInstance)
    {
        base.InitializeNewMob(gameObject);

        mobInstance.AddComponent<BattleAreaAttacker>();
    }
}
