using UnityEngine;
using System.Collections;

public class BattleAreaFactory : FactoryObject
{
    public override void Initialize()
    {
        base.Initialize();

        var mobProductionComponent = gameObject.AddComponent<BattleAreaFactoryMobProduction>();
        mobProductionComponent.ReproducableMob = MobCollection.Instance.BattleAreaMobLevel1;
    }

    public override void UpgradeFactory()
    {
        base.UpgradeFactory();
      
        FactoryBuilder.Instance.UpgrageFactoryPrefab(this);       
    }

    protected override void SetFactoryCalculator()
    {
        FactoryCalculator = gameObject.AddComponent<BattleAreaFactoryCalculator>();
    }

    protected override void SetFactoryImage()
    {
        var images = FactoryImageCollection.Instance;

        switch (this)
        {
            case var _ when Level == 1:
                FactoryImage = images.BattleAreaFactoryLevel1;
                break;
            case var _ when Level == 2 && Player == Player.Player1:
                FactoryImage = images.BattleAreaFactoryLevel2Player1;
                break;
            case var _ when Level == 2 && Player == Player.Player2:
                FactoryImage = images.BattleAreaFactoryLevel2Player2;
                break;
            case var _ when Level >= 3 && Player == Player.Player1:
                FactoryImage = images.BattleAreaFactoryLevel3Player1;
                break;
            case var _ when Level >= 3 && Player == Player.Player1:
                FactoryImage = images.BattleAreaFactoryLevel3Player2;
                break;
            default:
                break;
        }
    }
}
