using UnityEngine;
using System.Collections;

public class BattlePointFactoryMobProduction : BaseFactoryMobProduction
{
    protected override void InitializeNewMob(GameObject mobInstance)
    {
        base.InitializeNewMob(mobInstance);

        mobInstance.AddComponent<BattlePointAttacker>();
    }

    protected override void AddProgressBar()
    {
        base.AddProgressBar();

        ProgressBarControl.SetColor(UICollection.Instance.BattlePointFactoryProgressBarColor);
    }
}
