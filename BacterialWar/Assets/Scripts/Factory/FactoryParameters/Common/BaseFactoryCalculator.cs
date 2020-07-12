using UnityEngine;

public abstract class BaseFactoryCalculator : MonoBehaviour
{
    public abstract FactoryParameters GetParameters(int level);
}
