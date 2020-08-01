using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFactoryCalculator : MonoBehaviour
{
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
