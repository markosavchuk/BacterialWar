using System;
using UnityEngine;

public class FactoryManager : SingletonBase<FactoryManager>
{
    public GameObject CreateFactory()
    {
        var gameObject = new GameObject();
        //gameObject.transform.position = MapManager.Instance.Hexs[10, 5].transform.position;

        return gameObject;
    }
}
