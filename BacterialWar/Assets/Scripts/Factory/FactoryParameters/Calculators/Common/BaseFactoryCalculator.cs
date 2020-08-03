using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFactoryCalculator : MonoBehaviour
{
    protected const int AverageMobsAttackedWithRadius1 = 2;
    protected const int AverageMobLifetime = 3;
    protected const int HowManyStepsNeedToPayOffTheFactory = 10;

    protected static Dictionary<(Type Type, int Level), FactoryParameters> СachedParameters
        = new Dictionary<(Type Type, int Level), FactoryParameters>();

    protected FactoryParameters CheckForCaching(Type type, int level)
    {
        if (СachedParameters.ContainsKey((type, level)))
        {
            return СachedParameters[(type, level)];
        }
        else
        {
            return null;
        }
    }

    public abstract FactoryParameters GetParameters(int level);    
}
