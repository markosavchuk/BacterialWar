﻿using System;
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

        parameters.Health += 10 * level;
        parameters.Cost += 200 * level;
        parameters.Freeze = 0.5f;
        parameters.RiachRange += (level - 1);

        СachedParameters.Add((GetType(), level), parameters);

        return parameters;
    }
}