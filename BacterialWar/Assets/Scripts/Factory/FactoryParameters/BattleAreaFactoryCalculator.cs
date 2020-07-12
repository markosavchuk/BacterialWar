using System;
public class BattleAreaFactoryCalculator : BaseFactoryCalculator
{
    public override FactoryParameters GetParameters(int level)
    {
        var parameters = new FactoryParameters
        {
            Name = "Wave Attack Virus Generator",
            MobImage = MobImageCollection.Instance.BattleAreaMobLevel1
        };
        return parameters;
    }
}
