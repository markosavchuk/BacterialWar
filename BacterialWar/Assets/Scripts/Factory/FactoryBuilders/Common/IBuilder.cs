using System;
using UnityEngine;

public interface IBuilder
{
    GameObject GetFactoryPrefab(FactoryObject factory = null);
}
