using System;
using UnityEngine;

public class FreezeFactoryCalculator : BaseFactoryCalculator
{
    public override FactoryParameters GetParameters(int level)
    {
        var cached = CheckForCaching(GetType(), level);

        if (cached != null)
        {
            return cached;
        }

        var parameters = new FreezeFactoryParameters
        {
            Name = "Freeze Virus Generator",
        };

        switch (level)
        {
            case 1:
                parameters.MobImage = MobImageCollection.Instance.FreezeMobLevel1;
                break;
            case 2:
            default:
                parameters.MobImage = MobImageCollection.Instance.FreezeMobLevel2;
                break;
        }

        CalculateParameters(parameters, level);

        СachedParameters.Add((GetType(), level), parameters);

        return parameters;
    }

    private void CalculateParameters(FreezeFactoryParameters parameters, int level)
    {
        parameters.Health = 75 * Mathf.Pow(1.5f, level - 1);
        parameters.AttackSpeed = Mathf.RoundToInt(level * 0.6f);
        parameters.RiachRange = Mathf.RoundToInt(level * 0.75f);
        parameters.Defense = 10 * ((Mathf.Pow(0.9f, level*1.5f) - 1f) / (0.9f - 1f));
        parameters.GenetaionSpeed = 0.4f + ((level - 1) * 0.1f);
        parameters.FactoryHealth = parameters.Health * HowManyStepsNeedToPayOffTheFactory;
        parameters.Freeze = 0.5f * Mathf.Pow(1.5f, level - 1);

        var averageMobsAttackedInOneStep =
            (float)(2 * AverageMobsAttackedWithRadius1 + parameters.RiachRange - 1) / 2 * parameters.RiachRange;

        var damageSummary = 0;

        var defenseSummary = parameters.Health * (1 + (parameters.Defense / 100)) * (parameters.Freeze * averageMobsAttackedInOneStep);

        var valueSammary = (damageSummary + defenseSummary) * (1 + ((float)(parameters.RiachRange - 1) / 3));

        parameters.Cost = level == 1
            ? Mathf.RoundToInt(valueSammary * HowManyStepsNeedToPayOffTheFactory)
            : Mathf.RoundToInt(valueSammary * HowManyStepsNeedToPayOffTheFactory) - GetParameters(level - 1).Cost;

        parameters.RewardForDestroyingMob = defenseSummary;
    }
}