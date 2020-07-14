using System;
public class InfectionFactoryCalculator : BaseFactoryCalculator
{
    public override FactoryParameters GetParameters(int level)
    {
        var parameters = new FactoryParameters
        {
            Name = "Infectious Virus Generator",
        };

        switch (level)
        {
            case 1:
                parameters.MobImage = MobImageCollection.Instance.InfectionMobLevel1;
                break;
            case 2:
            default:
                parameters.MobImage = MobImageCollection.Instance.InfectionMobLevel2;
                break;
        }

        parameters.Health += 10 * level;
        parameters.Cost += 100 * level;
        parameters.Damage += 20 * level;
        parameters.RiachRange += (level - 1);

        return parameters;
    }
}