using System;
public class BattlePointFactoryCalculator : BaseFactoryCalculator
{
    public override FactoryParameters GetParameters(int level)
    {
        var parameters = new FactoryParameters
        {
            Name = "Point Attack Virus Generator",
        };

        switch (level)
        {
            case 1:
                parameters.MobImage = MobImageCollection.Instance.BattlePointMobLevel1;
                break;
            case 2:
                parameters.MobImage = MobImageCollection.Instance.BattlePointMobLevel2;
                break;
            case 3:
            default:
                parameters.MobImage = MobImageCollection.Instance.BattlePointMobLevel3;
                break;
        }

        parameters.Health += 10 * level;
        parameters.Cost += 100 * level;
        parameters.Damage += 20 * level;
        parameters.RiachRange += (level - 1);
        parameters.GenetaionSpeed += (level - 1);

        return parameters;
    }
}