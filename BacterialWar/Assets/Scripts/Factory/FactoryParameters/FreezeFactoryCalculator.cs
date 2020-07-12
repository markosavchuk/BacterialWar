using System;
public class FreezeFactoryCalculator : BaseFactoryCalculator
{
    public override FactoryParameters GetParameters(int level)
    {
        var parameters = new FactoryParameters
        {
            Name = "Freeze Virus Generator",
            MobImage = MobImageCollection.Instance.FreezeMobLevel1
        };
        return parameters;
    }
}