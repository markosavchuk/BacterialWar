using System;
public class InfectionFactoryCalculator : BaseFactoryCalculator
{
    public override FactoryParameters GetParameters(int level)
    {
        var parameters = new FactoryParameters
        {
            Name = "Infectious Virus Generator",
            MobImage = MobImageCollection.Instance.InfectionMobLevel1
        };
        return parameters;
    }
}