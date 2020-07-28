using UnityEngine;

public abstract class BaseFactoryCalculator : MonoBehaviour
{
    //todo add caching
    public abstract FactoryParameters GetParameters(int level);
}
