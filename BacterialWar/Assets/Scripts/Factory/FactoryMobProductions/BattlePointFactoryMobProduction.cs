using UnityEngine;
using System.Collections;

public class BattlePointFactoryMobProduction : BaseFactoryMobProduction
{
    private BattlePointAttacker _attacker;

    protected override void InitializeNewMob(GameObject mobInstance)
    {
        base.InitializeNewMob(mobInstance);

        var parameters = FactoryObject.Parameters as BattlePointFactoryParameters;

        _attacker = mobInstance.AddComponent<BattlePointAttacker>();
        _attacker.SetParameters(parameters);
    }

    protected override void AddProgressBar()
    {
        base.AddProgressBar();

        ProgressBarControl.SetColor(UICollection.Instance.BattlePointFactoryProgressBarColor);
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        var parameters = FactoryObject.Parameters as BattlePointFactoryParameters;

        _attacker.SetParameters(parameters);
    }
}
