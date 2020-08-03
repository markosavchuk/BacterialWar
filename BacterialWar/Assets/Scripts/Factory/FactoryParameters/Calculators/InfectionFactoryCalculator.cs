using System;
using UnityEngine;

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

        CalculateParameters(parameters, level);

        СachedParameters.Add((GetType(), level), parameters);

        return parameters;
    }

    private void CalculateParameters(InfectionFactoryParameters parameters, int level)
    {
        parameters.Health = 75 * Mathf.Pow(1.5f, level - 1);
        parameters.Infection = 20 * Mathf.Pow(1.75f, level - 1);
        parameters.AttackSpeed = Mathf.RoundToInt(level * 0.6f);
        parameters.RiachRange = Mathf.RoundToInt(level * 0.75f);
        parameters.Defense = 10 * ((Mathf.Pow(0.9f, level * 1.5f) - 1f) / (0.9f - 1f));
        parameters.GenetaionSpeed = 0.4f + ((level - 1) * 0.1f);
        parameters.FactoryHealth = parameters.Health * HowManyStepsNeedToPayOffTheFactory;

        var averageMobsAttackedInOneStep = 1;

        var damageSummary =
            parameters.Infection *
            parameters.AttackSpeed *
            parameters.GenetaionSpeed *
            averageMobsAttackedInOneStep *
            AverageMobLifetime;

        var defenseSummary = parameters.Health * (1 + (parameters.Defense / 100));

        var valueSammary = (damageSummary + defenseSummary) * (1 + ((float)(parameters.RiachRange - 1) / 3));

        parameters.Cost = level == 1
            ? Mathf.RoundToInt(valueSammary * HowManyStepsNeedToPayOffTheFactory)
            : Mathf.RoundToInt(valueSammary * HowManyStepsNeedToPayOffTheFactory) - GetParameters(level - 1).Cost;

        parameters.RewardForDestroyingMob = defenseSummary;
    }
}