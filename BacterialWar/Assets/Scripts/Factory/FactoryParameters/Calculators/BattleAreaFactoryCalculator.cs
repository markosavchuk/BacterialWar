﻿using System;
public class BattleAreaFactoryCalculator : BaseFactoryCalculator
{
    public override FactoryParameters GetParameters(int level)
    {
        var cached = CheckForCaching(GetType(), level);

        if (cached != null)
        {
            return cached;
        }

        var parameters = new BattleAreaFactoryParameters
        {
            Name = "Wave Attack Virus Generator",
        };

        switch (level)
        {
            case 1:
                parameters.MobImage = MobImageCollection.Instance.BattleAreaMobLevel1;
                break;
            case 2:
                parameters.MobImage = MobImageCollection.Instance.BattleAreaMobLevel2;
                break;
            case 3:
            default:
                parameters.MobImage = MobImageCollection.Instance.BattleAreaMobLevel3;
                break;
        }

        parameters.Health += 10 * level;
        parameters.Cost += 120 * level;
        parameters.WaveDamage += 1 * level;
        parameters.RiachRange += (level - 1);

        СachedParameters.Add((GetType(), level), parameters);

        return parameters;
    }
}
