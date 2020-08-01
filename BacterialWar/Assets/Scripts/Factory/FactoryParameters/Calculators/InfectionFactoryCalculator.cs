using System;
public class InfectionFactoryCalculator : BaseFactoryCalculator
{
    public override FactoryParameters GetParameters(int level)
    {
        var cached = CheckForCaching(GetType(), level);

        if (cached != null)
        {
            return cached;
        }

        var parameters = new InfectionFactoryParameters
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
        parameters.Cost += 110 * level;
        parameters.Infection += 2 * level;
        parameters.RiachRange += (level - 1);

        СachedParameters.Add((GetType(), level), parameters);

        return parameters;
    }
}