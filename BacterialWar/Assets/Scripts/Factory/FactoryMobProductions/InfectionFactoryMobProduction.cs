using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionFactoryMobProduction : BaseFactoryMobProduction
{
    private InfectionAttacker _attacker;

    protected override void InitializeNewMob(GameObject mobInstance)
    {
        base.InitializeNewMob(mobInstance);

        var parameters = FactoryObject.Parameters as InfectionFactoryParameters;

        _attacker = mobInstance.AddComponent<InfectionAttacker>();
        _attacker.SetParameters(parameters);
    }

    protected override void AddProgressBar()
    {
        base.AddProgressBar();

        ProgressBarControl.SetColor(UICollection.Instance.InfectionFactoryProgressBarColor);
    }
}
