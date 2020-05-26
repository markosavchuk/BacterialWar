using UnityEngine;
using System.Collections;

public class BattleAreaFactoryMobProduction : BaseFactoryMobProduction
{
    protected override void MakeNewMob()
    {
        base.MakeNewMob();

        if (NewMobInstance == null)
        {
            return;
        }
    }
}
