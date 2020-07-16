using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeFactoryMobProduction : BaseFactoryMobProduction
{
    private FreezeAttacker _attacker;

    protected override void InitializeNewMob(GameObject mobInstance)
    {
        base.InitializeNewMob(mobInstance);

        var parameters = FactoryObject.Parameters as FreezeFactoryParameters;

        _attacker = mobInstance.AddComponent<FreezeAttacker>();
        _attacker.SetParameters(parameters);
    }

    protected override void AddProgressBar()
    {
        base.AddProgressBar();

        ProgressBarControl.SetColor(UICollection.Instance.FreezeFactoryProgressBarColor);
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        var parameters = FactoryObject.Parameters as FreezeFactoryParameters;
        _attacker.SetParameters(parameters);
    }
}
