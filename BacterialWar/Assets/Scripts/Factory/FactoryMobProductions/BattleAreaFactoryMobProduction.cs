using UnityEngine;
using System.Collections;

public class BattleAreaFactoryMobProduction : BaseFactoryMobProduction
{
    private BattleAreaAttacker _attacker;

    protected override void InitializeNewMob(GameObject mobInstance)
    {
        base.InitializeNewMob(gameObject);

        var parameters = FactoryObject.Parameters as BattleAreaFactoryParameters;

        _attacker = mobInstance.AddComponent<BattleAreaAttacker>();
        _attacker.SetParameters(parameters);
    }

    protected override void AddProgressBar()
    {
        base.AddProgressBar();
        
        ProgressBarControl.SetColor(UICollection.Instance.BattleAreaFactoryProgressBarColor);
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        var parameters = FactoryObject.Parameters as BattleAreaFactoryParameters;

        _attacker.SetParameters(parameters);
    }
}
