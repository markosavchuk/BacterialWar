using System;
public class BattlePointFactoryCalculator : BaseFactoryCalculator
{
    public override FactoryParameters GetParameters(int level)
    {
        var parameters = new FactoryParameters
        {
            Name = "Point Attack Virus Generator",
            MobImage = MobImageCollection.Instance.BattlePointMobLevel1
        };
        return parameters;
    }
}